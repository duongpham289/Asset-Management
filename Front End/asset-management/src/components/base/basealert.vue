<template>
  <div class="m-alert">
    <div class="m-alert-container">
      <div class="alert-content">
        <div class="content-left">
          <div class="alert"></div>
        </div>
        <div class="content-right">
          <slot></slot>
        </div>
      </div>
      <!-- Thông báo khi xóa -->
      <div v-if="alertType == 'remove'" class="alert-button">
        <BaseButton
          @click="this.$emit('closeAlert')"
          type="outline-button"
          buttonTitle="Không"
        ></BaseButton>
        <BaseButton
          ref="focusBtn"
          @click="this.$emit('removeBtn')"
          buttonTitle="Xóa"
        ></BaseButton>
      </div>

      <!-- Thông báo khi hủy và không có sự thay đổi -->
      <div v-else-if="alertType == 'cancel'" class="alert-button">
        <BaseButton
          @click="this.$emit('closeAlert')"
          type="outline-button"
          buttonTitle="Không"
        ></BaseButton>
        <BaseButton
          ref="focusBtn"
          @click="this.$emit('closeDialog')"
          buttonTitle="Hủy bỏ"
        ></BaseButton>
      </div>

      <!-- Thông báo khi hủy và có sự thay đổi  -->
      <div v-else-if="alertType == 'cancelChange'" class="alert-button">
        <BaseButton
          @click="this.$emit('closeAlert')"
          type="outline-button"
          buttonTitle="Hủy bỏ"
        ></BaseButton>
        <BaseButton
          @click="this.$emit('closeDialog')"
          type="sub-button"
          buttonTitle="Không lưu"
        ></BaseButton>
        <BaseButton
          ref="focusBtn"
          @click="this.$emit('onSubmit')"
          buttonTitle="Lưu"
        ></BaseButton>
      </div>

      <!-- Thông báo lỗi, chỉ có thể đóng -->
      <div v-else class="alert-button">
        <BaseButton
          ref="focusBtn"
          @click="this.$emit('closeAlert')"
          buttonTitle="Đóng"
        ></BaseButton>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: 'the-alert',
  props: ['alertType'],
  mounted() {
    // focus
    this.$refs.focusBtn.setFocus();
  },

  methods: {},
};
</script>
<style></style>