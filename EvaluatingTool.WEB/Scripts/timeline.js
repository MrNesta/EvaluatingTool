$(document).ready(function () {
    var test_id = parseInt(window.location.pathname.replace(/\D+/g, ""));

    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: " /Home/Timeline/" + test_id,
        success: function (data) {
            google.charts.load('current', { 'packages': ['timeline'] });
            google.charts.setOnLoadCallback(drawChart);

            function drawChart() {
                var container = document.getElementById('timeline');
                var chart = new google.visualization.Timeline(container);
                var dataTable = new google.visualization.DataTable();

                dataTable.addColumn({ type: 'string', id: 'Id' });
                dataTable.addColumn({ type: 'string', id: 'Page' });
                dataTable.addColumn({ type: 'number', id: 'Start' });
                dataTable.addColumn({ type: 'number', id: 'End' });

                dataTable.addRows(data);

                var options = {
                    chart: { title: 'Responce time chart' },
                    timeline: { singleColor: '#4285F4' }
                };

                chart.draw(dataTable, options);
            }
        }
    });
});