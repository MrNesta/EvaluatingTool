$(document).ready(function () {
    var test_id = parseInt(window.location.pathname.replace(/\D+/g, ""));

    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        dataType: 'json',
        url: " /Home/BarTimeline/" + test_id,
        success: function (data) {
            google.charts.load('current', { 'packages': ['bar'] });
            google.charts.setOnLoadCallback(drawChart);

            function drawChart() {
                var dataTable = new google.visualization.DataTable();

                dataTable.addColumn({ type: 'string', id: 'Page' });
                dataTable.addColumn({ type: 'number', id: 'End' });
                dataTable.addRows(data);
                var options = {
                    legend: { position: 'none' },
                    chart: { title: 'Responce time chart' },
                    bars: 'horizontal',
                    hAxis: { format: 'decimal' },
                    axes: {
                        x: {
                            0: { label: 'milliseconds' } // Top x-axis.
                        }
                    }
                };

                var chart = new google.charts.Bar(document.getElementById('timeline'));
                chart.draw(dataTable, options);
            }
        }
    });
});