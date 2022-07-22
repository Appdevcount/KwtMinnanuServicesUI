 $(function () {
            // Wire up your DatePicker
            $('.datepicker').datepicker({
                //  format: "dd MM yyyy - hh:ii",
                //autoclose: true,
                ////todayBtn: true,
                //changeMonth: true,
                //changeYear: true
                //   startDate: "2013-02-14 10:00",
                //   minuteStep: 10
                dateFormat: 'mm/dd/yyyy',
                pickDate: true,
                autoclose: true,
                todayBtn: true
            }).attr('readonly', 'readonly');
        });