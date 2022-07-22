function fnCIVILIDValidate(CivilId) {
    if (CivilId != null && CivilId != "")
    {
        var dServerDate = new Date();
        var calculation = 0;
        var firstPart = parseInt(CivilId.substring(0, 1));
        var yearPart = parseInt(CivilId.substring(1, 3));
        var monthPart = parseInt(CivilId.substring(3, 5));
        var dayPart = parseInt(CivilId.substring(5, 7));
        if (isNaN(firstPart) || isNaN(yearPart) || isNaN(monthPart) || isNaN(dayPart) || firstPart < 0 || yearPart < 0 || monthPart < 0 || dayPart < 0   )
        {
            console.log('civilid isNaN');
            //CivilId.txtCivilID.focus();
            return false;
        }
        if (firstPart == '3') {
            yearPart = 2000 + yearPart;
            if (yearPart > dServerDate.getFullYear())
            {
                console.log('year 2 invalid case 1' + dServerDate.getFullYear());
                //CivilId.txtCivilID.focus();
                return false;
            }
            else if (yearPart == dServerDate.getFullYear() && monthPart >= dServerDate.getMonth())
            {
                console.log('year 2 invalid case 2' + dServerDate.getFullYear());
                //CivilId.txtCivilID.focus();
                return false;
            }
            console.log('good year 3');
        }
        else if (firstPart == '2') {
            yearPart = 1900 + yearPart;
            console.log('good year 2');
        }
        else {
            console.log('first part not 2or 3');
            //CivilId.txtCivilID.focus();
            return false;
        }

        if ((monthPart > 12 || monthPart < 1) || (dayPart > 31 || dayPart < 1))
        {
            console.log('invalid month');
            //CivilId.txtCivilID.focus();
            return false;
        }

        calculation = calculation + parseInt(2 * CivilId.charAt(0)) + parseInt(1 * CivilId.charAt(1)) + parseInt(6 * CivilId.charAt(2)) + parseInt(3 * CivilId.charAt(3)) + parseInt(7 * CivilId.charAt(4)) + parseInt(9 * CivilId.charAt(5)) + parseInt(10 * CivilId.charAt(6)) + parseInt(5 * CivilId.charAt(7)) + parseInt(8 * CivilId.charAt(8)) + parseInt(4 * CivilId.charAt(9)) + parseInt(2 * CivilId.charAt(10));
        console.log(calculation);
        calculation = calculation % 11;
        console.log(calculation);
        calculation = 11 - calculation;
        console.log(calculation);
        console.log(parseInt(CivilId.charAt(11)));
        if (calculation != parseInt(CivilId.charAt(11))) {
            console.log('canculation invalid');
            //CivilId.txtCivilID.focus();
            return false;
        }
        else {
            console.log('correct civilid');
            return true;
        }
    }
}
