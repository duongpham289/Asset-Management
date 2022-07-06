<template>
  <div id="frmLogin">
    <div class="login w-100 h-100">
      <div class="grid-login">
        <div class="half-ctn img-login"></div>
        <div class="half-ctn">
          <div class="main-login-ctn">
          
            <div class="logo"></div>
            <div class="logo-text">
              Đăng nhập để làm việc với <b>MISA QLTS</b>
            </div>
            <form @submit.prevent="submit" id="normal-login">
              <div class="grid-login-normal">
                <div class="username-login">
                  <MISAInput
                    id="iptUserName"
                    ref="userName"
                    :tag="'userName'"
                    class="input-login"
                    
                    placeholder="Username, email hoặc số điện thoại"
                    @focus="this.showError = false"
                    @changeInput="changeInput"
                  />
                </div>
                <div class="password-login">
                  <MISAInput
                    :tag="'passWord'"
                    ref="passWord"
                    id="iptPassword"
                    type="passWord"
                    
                    placeholder="Mật khẩu"
                    class="input-login"
                    @focus="this.showError = false"
                    @changeInput="changeInput"
                  />
                  <div class="eye on-eye"></div>
                </div>

                <div class="button-login" style="margin-top: 20px; margin-bottom:10px ">
                  <button type="submit" class="button button-text" id="login">
                    Đăng nhập
                  </button>
                </div>
              </div>
            </form>
              <div v-if="this.showError" class="wrong-login">
              <div style="text-align: left; font-weight: 700; color: red;">
                Sai tài khoản hoặc mật khẩu, vui lòng đăng nhập lại.
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<style scoped>
</style>