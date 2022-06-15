<template>
  <button :disabled="disabled" class="m-button" :class="type" ref="button">
    {{ buttonTitle }}
  </button>
</template>
<script>
export default {
  name: 'the-button',
  props: ['buttonTitle', 'type', 'disabled', 'isValid'],
  methods: {
    setFocus() {
      this.$refs.button.focus();
    },
  },
};
</script>
<style></style>