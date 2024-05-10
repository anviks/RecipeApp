<script setup lang="ts">

</script>

<template>
    <h1>Register as a user</h1>

    <div class="row">
        <div class="col-md-4">
            <form id="registerForm" method="post" action="/Identity/Account/Register">
                <h2>Create a new account.</h2>
                <hr>

                <div class="form-floating mb-3">
                    <input class="form-control" autocomplete="username" aria-required="true" placeholder="" type="text" 
                           id="Input_Username" maxlength="64"
                           name="Input.Username" value="">
                    <label for="Input_Username">Username</label>
                </div>
                <div class="form-floating mb-3">
                    <input class="form-control" autocomplete="username" aria-required="true" placeholder="" type="email" 
                           id="Input_Email" name="Input.Email" value="">
                    <label for="Input_Email">Email</label>
                </div>
                <div class="form-floating mb-3">
                    <input class="form-control" autocomplete="new-password" aria-required="true" placeholder=""
                           type="password" id="Input_Password" maxlength="100"
                           name="Input.Password">
                    <label for="Input_Password">Password</label>
                </div>
                <div class="form-floating mb-3">
                    <input class="form-control" autocomplete="new-password" aria-required="true" placeholder=""
                           type="password" id="Input_ConfirmPassword" name="Input.ConfirmPassword">
                    <label for="Input_ConfirmPassword">Confirm password</label>
                </div>
                <button id="registerSubmit" type="submit" class="w-100 btn btn-lg btn-primary">Register</button>
            </form>
        </div>
        <div class="col-md-6 col-md-offset-2">
            <section>
                <h3>Use another service to register.</h3>
                <hr>
                <div>
                    <p>
                        There are no external authentication services configured. See this <a
                        href="https://go.microsoft.com/fwlink/?LinkID=532715">article
                        about setting up this ASP.NET application to support logging in via external services</a>.
                    </p>
                </div>
            </section>
        </div>
    </div>
</template>

<style scoped></style>