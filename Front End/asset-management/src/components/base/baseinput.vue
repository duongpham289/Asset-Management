<template>
  <input
    ref="input"
    class="m-input"
    title=""
    :type="type"
    :name="name"
    :class="classParent"
    :maxlength="maxlength"
    :placeholder="placeholder"
    :disabled="disabled"
    :required="required"
    :isNumber="isNumber"
    @keypress="onlyNumber"
    @blur="outFocus($event)"
    @input="onChangeHandler"
    :value="this.modelValue"
  />
</template>
<script>
export default {
  name: "the-input",

  props: [
    "number", //style number chuyền từ component cha
    "placeholder",
    "disabled",
    "classParent",
    "type",
    "modelValue",
    "maxlength",
    "required",
    "name",
    "isNumber",
  ],

  emits: [
    "setIsValid",
    "update:modelValue",
    "keypress",
    "keydown.down",
    "keydown.up",
    "blur",
  ],

  updated() {
    this.validateRequired();
  },

  methods: {
    // Nhận thay đổi của component cha
    onChangeHandler(e) {
      e.preventDefault();
      this.$emit("update:modelValue", e.target.value);
    },

    /**
     * Mô tả : Chỉ nhận input
     * @param
     * @return
     * Created by: Lê Thiện Tuấn - MF1118
     * Created date: 23:44 02/05/2022
     */
    onlyNumber($event) {
      if (this.isNumber == true) {
        let keyCode = $event.keyCode ? $event.keyCode : $event.which;
        if ((keyCode < 48 || keyCode > 57) && keyCode !== 46) {
          $event.preventDefault();
        }
      }
    },

    /**
     * Mô tả : focus vào ô đầu tiên
     * @param
     * @return
     * Created by: Lê Thiện Tuấn - MF1118
     * Created date: 19:46 05/05/2022
     */
    setFocus() {
      this.$refs.input.focus();
    },

    /**
     * Mô tả : kiểm tra input có trống không
     * @param
     * @return
     * Created by: Lê Thiện Tuấn - MF1118
     * Created date: 19:29 05/05/2022
     */
    outFocus($event) {
      this.validateRequired();
      this.$emit("blur", $event);
    },

    validateRequired() {
      if (
        this.required &&
        (this.modelValue === undefined || this.modelValue === "")
      ) {
        this.$refs.input.classList.add("m-input-error");
      } else {
        this.$refs.input.classList.remove("m-input-error");
      }
    },
  },

  data() {
    return {};
  },
};
</script>
<style></style>
