<template>
  <div class="m-layout">
    <HUSTNavbar @onCloseNavBar="onCloseNavBar" :navBarWidth="navBarWidth" />
    <div
      class="m-main"
      :style="{ width: 'calc(100% - ' + this.navBarWidth + 'px)' }"
    >
      <HUSTHeader />
      <!-- <MISAContent></MISAContent> -->
      <router-view></router-view>
    </div>
  </div>
</template>
<script>
export default {
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
<style></style>