<template>
  <div class="m-header">
    <div class="header-left">Danh sách tài sản</div>
    <div class="header-right">
      <div class="header-right-title">Sở tài chính</div>
      <!-- option -->
      <div class="header-year">
        <div class="header-year-text">Năm</div>
        <div class="header-year-content">{{ year }}</div>
        <div class="year-btn-container">
          <div class="up"></div>
          <div class="down"></div>
        </div>
      </div>
      <!--  -->
      <div class="header-icon">
        <div class="bell"></div>
      </div>
      <div class="header-icon">
        <div class="option"></div>
      </div>
      <div class="header-icon">
        <div class="question"></div>
      </div>
      <div class="header-icon-user" @click="openDropdown" v-on:clickout="this.open = false">
        <div class="header-icon">
          <div class="user-logo"></div>
        </div>
        <div class="down">
          <div class="down-content" v-if="open">
            <div @click="logout">Log Out</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
/* The container <div> - needed to position the dropdown content */
.down {
  position: relative;
  /* display: inline-block; */
  z-index: 1000000;
}

/* down Content (Hidden by Default) */
.down-content {
  /* display: none; */
  position: absolute;
  background-color: #b7b7b7;
  min-width: 78px;
  box-shadow: 0px 8px 16px 0px rgba(0, 0, 0, 0.2);
  z-index: 1;
  position: absolute;
  top: 13px;
  right: -10px;
  border-radius: 10px;
}

/* Links inside the down */
.down-content div {
  color: black;
  padding: 12px 16px;
  text-decoration: none;
  display: block;
}

/* Change color of down links on hover */
.down-content div:hover {
  background-color: #f1f1f1;
  cursor: pointer;
}

/* Show the down menu on hover */
/* .down:hover .down-content {
  display: block;
} */
</style>

<script>
// import compoment

import { mapGetters, mapMutations } from "vuex";
import axios from "axios";
import "clickout-event";
export default {
  name: "the-header",
  components: {},
  computed: {
    ...mapGetters(["user"]),
  },

  methods: {
    ...mapMutations(["setUser"]),
    openDropdown() {
      this.open = !this.open;
    },
    async logout() {
      try {
        const res = await axios.get("http://localhost:5290/api/v1/Users/logout");
        this.$router.push("/login");
        this.setUser(res.data);
      } catch (error) {
        console.log(error);
      }
    },
  },
  data() {
    return {
      year: new Date().getFullYear(),
      open: false,
    };
  },
};
</script>
