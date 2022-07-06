<template>
  <div class="m-login">
    <div class="grip-login">
      <div class="half-ctn left-grip"></div>
      <div class="half-ctn right-grip">
        <div class="main-login-ctn">
          <div class="logo"></div>
          <div class="logo-text">
            Đăng nhập để làm việc với <b>MISA QLTS</b>
          </div>

          <div id="normal-login">
            <form class="grid-login-normal">
              <div class="username-login">
                <input
                  id="iptUserName"
                  class="input-login"
                  placeholder="Username, email hoặc số điện thoại"
                  v-model="this.loginForm.email"
                />
              </div>
              <div class="password-login">
                <input
                  autocomplete
                  id="iptPassword"
                  type="password"
                  placeholder="Mật khẩu"
                  class="input-login"
                  v-model="this.loginForm.password"
                />
                <div class="eye on-eye"></div>
              </div>

              <div class="button-login" style="margin-top: 20px">
                <button @click.prevent="onLoginClick" class="button" id="login">
                  Đăng nhập
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import axios from 'axios';
import { mapGetters, mapMutations } from 'vuex';

export default {
  computed: {
    ...mapGetters(['user']),
  },

  methods: {
    ...mapMutations(['setUser']),
    /**
     * Mô tả : login
     * @param
     * @return
     * Created by: Lê Thiện Tuấn - MF1118
     * Created date: 16:02 31/05/2022
     */
    async onLoginClick() {
      try {
        const res = await axios.post('http://localhost:5290/api/v1/Users/Login', this.loginForm, {
          headers: {
            'Access-Control-Allow-Origin': '*',
            'Content-Type': 'application/json',
          },
        });
        if (res.status == 200) {
          this.$router.push('/asset');
          this.setUser(res.data);
        }
      } catch (error) {
        console.log(error);
      }
    },
  },

  data() {
    return {
      loginForm: {
        email: '',
        password: '',
      },
    };
  },
};
</script>
<style></style>
