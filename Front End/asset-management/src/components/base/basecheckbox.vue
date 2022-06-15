<template>
  <!-- Nếu true thì nhận giá trị css -->
  <div
    class="m-checkbox"
    :class="{
      'm-checkbox-selected': isChecked,
    }"
  ></div>
</template>
<script>
export default {
  name: "the-checkbox",
  props: ["checked"],
  watch: {
    checked: function (newValue) {
      this.isChecked = newValue;
    },
  },
  data() {
    return {
      isChecked: false,
    };
  },
};
</script>
<style></style>