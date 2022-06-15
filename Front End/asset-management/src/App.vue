<template>
  <div class="m-layout">
    <TheNavbar></TheNavbar>
    <div class="m-main">
      <TheHeader></TheHeader>
      <TheContent></TheContent>
    </div>
  </div>
</template>

<script>
import TheNavbar from "./components/layout/TheNavbar.vue";
import TheContent from "./components/layout/TheContent.vue";
import TheHeader from "./components/layout/TheHeader.vue";
export default {
  name: "App",

  components: {
    TheNavbar,
    TheContent,
    TheHeader,
  },
};
</script>

<style>
@import url(./css/main.css);
</style>
