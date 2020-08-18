// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $("#AddPerson").click(function () {
        var i = parseInt($('#totalEmployees').val());
        if (i+1 > 1) {
            $("#RemovePerson").show();
        }
        $('#totalEmployees').val(i+1);
        var FNameHTML = '<input class="form-control" placeholder="First Name" type="text" name="EmployeesMisc[' + i + '].FirstName" value="" />';
        var LNameHTML = '<input class="form-control" placeholder="Last Name" type="text" name="EmployeesMisc[' + i + '].LastName" value="" />';
        var TitleHTML = '<select class="form-control" placeholder="Unit" type="list" name="EmployeesMisc[' + i + '].Title" value=""> <option value="" disabled selected>Unit</option> '
            + '<option> Center for Internal Auditing</option >'
            + '<option>Department of Public Administration</option>'
            + '<option>Economics & Policy Research Group</option> '
            + '<option>Executive Education</option>'
            + '<option>External Relations</option>'
            + '<option>Flores MBA Program</option>'
            + '<option>Information Technology Group</option>'
            + '<option>Office of Advancement</option> '
            + '<option>Office of Business Student Success</option> '
            + '<option>Office of the Dean</option> '
            + '<option>Professional Sales Institute</option> '
            + '<option>Stephenson Entrepreneurship Institute</option></select > ';
        var RoomNumberHTML = '<input class="form-control" placeholder="Room Number" type="number" name="EmployeesMisc[' + i + '].RN" value="" />';
        $('#EmployeeSection').append('<div class="row ' + i + '"><div class= "col">' + FNameHTML + '</div><div class= "col">' + LNameHTML + '</div><div class= "col">' + TitleHTML + '</div><div class= "col">' + RoomNumberHTML + '</div></div>'); 
    });
    $("#RemovePerson").click(function () {
        var i = parseInt($('#totalEmployees').val()) - 1;
        if (i <= 1) {
            $("#RemovePerson").hide();
        }
        $('#totalEmployees').val(i);
         $('#EmployeeSection div.' + i).remove();
    });
    if (parseInt($('#totalEmployees').val()) <= 1) {
        $("#RemovePerson").hide();
    }
});