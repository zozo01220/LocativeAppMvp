window.renderRevenueChart = (labels, data) => {

    const canvas = document.getElementById("revenueChart");
    if (!canvas) return;

    if (window.revenueChartInstance) {
        window.revenueChartInstance.destroy();
    }

    window.revenueChartInstance = new Chart(canvas, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Revenus (€)',
                data: data,
                backgroundColor: '#0d6efd'
            }]
        },
        options: {
            responsive: true,
            plugins: {
                legend: { display: false }
            }
        }
    });

    console.log("📊 Revenue chart rendu");
};

window.renderPlansChart = (free, pro, business) => {
    console.log("🔥 dashboard.js OK");

    const canvas = document.getElementById("plansChart");
    if (!canvas) return;

    if (window.plansChartInstance) {
        window.plansChartInstance.destroy();
    }

    window.plansChartInstance = new Chart(canvas, {
        type: 'doughnut',
        data: {
            labels: ['Free', 'Pro', 'Business'],
            datasets: [{
                data: [free, pro, business]
            }]
        }
    });
};