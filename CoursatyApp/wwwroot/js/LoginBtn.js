$(function () {
    var loginBtn = $("#UserLoginModal button[name='login']").click(OnUserLoginClick);

    function OnUserLoginClick() {
        var Url = "UserAuth/login";
        var AntiForgeryToken = $("#UserLoginModal input[name='__RequestVerificationToken']").val();
        var userEmail = $("#UserLoginModal input[name='Email']").val();
        var userPass = $("#UserLoginModal input[name='Password']").val();
        var userRememberMe = $("#UserLoginModal input[name='RememberMe']").prop('checked');

        var UserInputs = {
            __RequestVerificationToken: AntiForgeryToken,
            Email: userEmail,
            Password: userPass,
            RememeberMe: userRememberMe
        }

        $.ajax({
            type: "POST",
            url: Url,
            data: UserInputs,
            success: function (data) { //call back function
                var parsed = $.parseHTML(data);
                var hasErrors = $(parsed).find("input[name='LoginInValid']").val() == "true";
                if (hasErrors == true) { //Fail Login Attempt
                    $("#UserLoginModal").html(data);
                    userLoginBtn = $("#UserLoginModal button[name='login']").click(OnUserLoginClick);

                    var form = $("#UserLoginForm");
                    $(form).removeData("validator");
                    $(form).removeData("unobtrusiveValidation");
                    $.validator.unobtrusive.parse(form);

                } else {
                    location.href = 'Home/Index';
                }
            },
            error: function (xhr, ajaxOptions, thrownError) {
                console.error(thrownError + "\r\n" + xhr.statusCode + "\r\n" + xhr.responseText);
            }
        });
    }
})