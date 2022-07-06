<template>
  <div class="datepicker-container">
    <Datepicker
      ref="datepicker"
      cancelText="Hủy"
      selectText="Chọn"
      format="dd/MM/yyyy"
      :enableTimePicker="false"
      :maxDate="new Date()"
      textInput
      autoApply
      @blur="setBlur"
      @focus="setFocus"
      v-model="date"
      @update:modelValue="handleDate"
      :inputClassName="inputClassName"
      :name="name"
      :required="required"
    >
    </Datepicker>
    <div v-if="this.errorMsg" class="m-input-msg">{{ errorMsg }}</div>
    <div class="datepicker-icon"></div>
  </div>
</template>
<script>
import Datepicker from "@vuepic/vue-datepicker";

export default {
  name: "the-date-picker",
  components: { Datepicker },

  props: ["modelValue", "name", "required"],

  // beforeMount() {
  //   this.date = this.modelValue;
  // },
  methods: {
    /**
     * Mô tả : câp nhât giá trị v-model
     * @param
     * @return
     * Created by: Lê Thiện Tuấn - MF1118
     * Created date: 00:35 31/05/2022
     */
    async handleDate(value) {
      this.date = value;
      await this.$emit("update:modelValue", this.date);
    },

    /**
     * Mô tả : Xử lí sự kiện focus
     * @param
     * @return
     * Created by: Lê Thiện Tuấn - MF1118
     * Created date: 01:12 31/05/2022
     */
    setFocus() {
      this.inputClassName = "m-input-focus";
      this.errorMsg = null;
    },

    /**
     * Mô tả : Xử lí sự kiện blur
     * @param
     * @return
     * Created by: Lê Thiện Tuấn - MF1118
     * Created date: 01:12 31/05/2022
     */
    setBlur() {
      this.inputClassName = "";
      this.validateRequired();
    },

    /**
     * Mô tả : Validate bắt buộc nhập
     * @param
     * @return
     * Created by: Lê Thiện Tuấn - MF1118
     * Created date: 01:12 31/05/2022
     */
    validateRequired() {
      if (this.required == true && (this.date == null || this.date == "")) {
        this.inputClassName = "m-input-error";
        this.createErrorMsg();
      } else {
        this.inputClassName = "";
        this.errorMsg = "";
      }
    },

    /**
     * Mô tả : Tạo error msg
     * @param
     * @return
     * Created by: Lê Thiện Tuấn - MF1118
     * Created date: 15:08 21/06/2022
     */
    createErrorMsg() {
      var inputName = this.name;
      if (inputName == undefined || inputName == "") {
        this.errorMsg = "Ô này không được để trống!";
      } else {
        this.errorMsg = `${inputName} không được để trống!`;
      }
    },
  },

  data() {
    return {
      date: this.modelValue,
      inputClassName: null,
      errorMsg: "",
    };
  },
};
</script>
<style></style>