function Confirm() {
    return confirm('Are you sure you want to delete this record???');
}
$(document).ready(function () {
    $('#example1').DataTable({
        'paging': true,
        'lengthChange': false,
        'searching': true,
        'ordering': true,
        'info': true,
        'autoWidth': false,
        dom: 'lfBrtip',
        buttons: [
            { extend: 'colvis', columns: ':not(.permanent)' },
            { extend: 'copy' },
            { extend: 'excel' },
            { extend: 'csv' },
            { extend: 'pdf' },
            { extend: 'print' }
        ]

    });
    $(".roleStatus").change(function () {
        var status = $(".roleStatus").val();

        console.log(status);
        if (status == 1) {
            // alert('Admin');
            $("#std_rol").hide();
            $("#teacher").hide();
            $("#parent").hide();
        } else if (status == 2) {
            $("#std_rol").show(500);
            $("#teacher").hide();
            $("#parent").hide();
        } else if (status == 3) {
            $("#teacher").show(500);
            $("#parent").hide();
            $("#std_rol").hide();
        } else if (status == 4) {
            $("#parent").show(500);
            $("#teacher").hide();
            $("#std_rol").hide();
        }
    });


});
//Timepicker
$('.timepicker').timepicker({
    showInputs: false
});