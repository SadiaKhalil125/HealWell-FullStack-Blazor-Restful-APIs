window.chartInterop = {
    renderAppointmentsChart: function (canvasId, chartData) {
        const ctx = document.getElementById(canvasId);
        if (!ctx) return;

        new Chart(ctx, {
            type: 'bar', // Changed from 'line' to 'bar'
            data: {
                labels: chartData.labels,
                datasets: [{
                    label: 'Appointments',
                    data: chartData.counts,
                   backgroundColor: 'rgba(54, 162, 235, 0.7)',
                }]
            },
            options: {
                responsive: true,
                scales: {
                    y: {
                        beginAtZero: true
                    }
                },
                plugins: {
                    legend: {
                        display: true
                    }
                }
            }
        });
    },

    renderSpecialtyChart: function (canvasId, chartData) {
        const ctx = document.getElementById(canvasId);
        if (!ctx) return;

        new Chart(ctx, {
            type: 'pie',
            data: {
                labels: chartData.labels,
                datasets: [{
                    data: chartData.counts,
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.6)',
                        'rgba(54, 162, 235, 0.6)',
                        'rgba(255, 206, 86, 0.6)',
                        'rgba(75, 192, 192, 0.6)',
                        'rgba(153, 102, 255, 0.6)',
                        'rgba(255, 159, 64, 0.6)'
                    ]
                }]
            },
            options: {
                responsive: true
            }
        });
    }
};
window.chartInterop = {
    renderAppointmentsChart: function (canvasId, data) {
        const ctx = document.getElementById(canvasId).getContext("2d");
        new Chart(ctx, {
            type: 'bar',
            data: {
                labels: data.labels,
                datasets: [{
                    label: 'Patient Growth',
                    data: data.counts,
                    backgroundColor: 'rgba(54, 162, 235, 0.6)',
                    borderColor: 'rgba(54, 162, 235, 1)',
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: { display: false },
                    title: {
                        display: false,
                        text: 'Patient Growth Over Time'
                    }
                },
                scales: {
                    y: {
                        beginAtZero: true,
                        title: { display: true, text: 'Patients' }
                    },
                    x: {
                        title: { display: true, text: 'Time' }
                    }
                }
            }
        });
    },

    renderSpecialtyChart: function (canvasId, data) {
        const ctx = document.getElementById(canvasId).getContext("2d");
        new Chart(ctx, {
            type: 'pie',
            data: {
                labels: data.labels,
                datasets: [{
                    label: 'Age Distribution',
                    data: data.counts,
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.6)',
                        'rgba(255, 206, 86, 0.6)',
                        'rgba(75, 192, 192, 0.6)',
                        'rgba(153, 102, 255, 0.6)'
                    ],
                    borderColor: [
                        'rgba(255, 99, 132, 1)',
                        'rgba(255, 206, 86, 1)',
                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)'
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        position: 'bottom',
                    },
                    title: {
                        display: false,
                        text: 'Age Group Breakdown'
                    }
                }
            }
        });
    }
};
