<template>
  <div>
    <div class="m-layout">
      <Navbar @onCloseNavBar="onCloseNavBar" :navBarWidth="navBarWidth" />
      <div
        class="m-main"
        :style="{ width: 'calc(100% - ' + this.navBarWidth + 'px)' }"
      >
        <Header />
        <Content></Content>
      </div>
    </div>
  </div>
</template>

<script>
import Header from './components/layout/TheHeader.vue';
import Navbar from './components/layout/TheNavbar.vue';
import Content from './components/layout/TheContent.vue';

export default {
  name: 'App',
  components: {
    Header,
    Content,
    Navbar,
  },

  methods: {
    /**
     * Đóng mở navbar
     */
    onCloseNavBar(isToggle) {
      if (isToggle == false) {
        this.navBarWidth = 226;
      } else {
        this.navBarWidth = 66;
      }
    },
  },

  data() {
    return {
      navBarWidth: 226,
    };
  },
};
</script>

<style>
@import url(./css/main.css);
</style>
