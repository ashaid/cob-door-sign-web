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
        var FNameHTML = '<input class="form-control" placeholder="First Name" type="text" name="EmployeesOffice[' + i + '].FirstName" value="" />';
        var LNameHTML = '<input class="form-control" placeholder="Last Name" type="text" name="EmployeesOffice[' + i + '].LastName" value="" />';
        var Title = '<input class="form-control" placeholder="Title" type="text" name="EmployeesOffice[' + i + '].Title" value="" />';
        var SecondTitle = '<input class="form-control" placeholder="Second Title (optional)" type="text" name="EmployeesOffice[' + i + '].SecondTitle" value="" />';
        var Professorship = '<input class="form-control" placeholder="Third Title (optional)" type="text" name="EmployeesOffice[' + i + '].Professorship" value="" />';
        $('#EmployeeSection').append('<div class="row ' + i + '"><div class= "col">' + FNameHTML + '</div><div class= "col">' + LNameHTML + '</div><div class= "col">' + Title + '</div><div class= "col">' + SecondTitle + '</div><div class= "col">' + Professorship + '</div></div>'); 
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