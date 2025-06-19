window.initializeServicesScroller = () => {
    document.addEventListener('DOMContentLoaded', function () {
        // DOM Elements
        const track = document.querySelector('.services-track');
        const serviceCards = document.querySelectorAll('.service-card');
        const scroller = document.querySelector('.services-scroller');

        if (!track || !scroller) return; // Ensure elements exist

        // Animate cards on load
        animateCards();

        // Track scroll events for accessibility
        scroller.addEventListener('scroll', function () {
            scroller.setAttribute('aria-busy', 'true');
            setTimeout(() => {
                scroller.setAttribute('aria-busy', 'false');
            }, 100);
        });

        // Keyboard navigation
        scroller.addEventListener('keydown', function (e) {
            if (e.key === 'ArrowLeft') {
                track.scrollBy({ left: -300, behavior: 'smooth' });
            } else if (e.key === 'ArrowRight') {
                track.scrollBy({ left: 300, behavior: 'smooth' });
            }
        });

        // Touch events for mobile
        let touchStartX = 0;
        let touchEndX = 0;

        scroller.addEventListener('touchstart', function (e) {
            touchStartX = e.changedTouches[0].screenX;
        }, { passive: true });

        scroller.addEventListener('touchend', function (e) {
            touchEndX = e.changedTouches[0].screenX;
            handleSwipe();
        }, { passive: true });

        // Functions
        function animateCards() {
            serviceCards.forEach((card, index) => {
                card.classList.add('animate');
                card.style.animationDelay = `${index * 0.1}s`;
            });
        }

        function handleSwipe() {
            const difference = touchStartX - touchEndX;
            if (difference > 50) {
                track.scrollBy({ left: 300, behavior: 'smooth' }); // Swipe left
            } else if (difference < -50) {
                track.scrollBy({ left: -300, behavior: 'smooth' }); // Swipe right
            }
        }
    });
};
// wwwroot/js/site.js
function scrollToBottom(element) {
    element.scrollTop = element.scrollHeight;
}

// Make it available globally
window.scrollToBottom = scrollToBottom;

function submitPayment() {
    // Validate form
    if (!validateForm()) {
        return;
    }

    // Show loading state
    const btn = document.querySelector('.btn-pay');
    btn.innerHTML = '<i class="fas fa-spinner fa-spin"></i> Processing...';
    btn.disabled = true;

    // Simulate payment processing
    setTimeout(function () {
        // Redirect to confirmation page
        window.location.href = '/confirmation';
    }, 2000);
}

function validateForm() {
    // Basic validation
    const requiredFields = ['cardNumber', 'expiry', 'cvv', 'emailConfirm', 'phoneConfirm'];

    for (const fieldId of requiredFields) {
        const field = document.getElementById(fieldId);
        if (!field.value.trim()) {
            alert(`Please fill in the ${field.labels[0].textContent} field`);
            field.focus();
            return false;
        }
    }

    // Validate email match (in real app would compare with original)
    if (document.getElementById('emailConfirm').value !== "john@example.com") {
        alert("Email confirmation doesn't match our records");
        return false;
    }

    return true;
}

// Format card number as user types
document.getElementById('cardNumber').addEventListener('input', function (e) {
    let value = e.target.value.replace(/\s+/g, '');
    if (value.length > 0) {
        value = value.match(new RegExp('.{1,4}', 'g')).join(' ');
    }
    e.target.value = value;
});

// Format expiry date as user types
document.getElementById('expiry').addEventListener('input', function (e) {
    let value = e.target.value.replace(/\D/g, '');
    if (value.length > 2) {
        value = value.substring(0, 2) + '/' + value.substring(2, 4);
    }
    e.target.value = value;
});
// Apply saved theme on page load
document.addEventListener('DOMContentLoaded', function () {
    const savedTheme = localStorage.getItem('theme');
    if (savedTheme === 'dark') {
        document.body.classList.add('dark-theme');
    }
});

// Helper function for Blazor to call
window.applyTheme = function (theme) {
    if (theme === 'dark') {
        document.body.classList.add('dark-theme');
    } else {
        document.body.classList.remove('dark-theme');
    }
};