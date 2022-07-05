<template>
  <div class="m-toast">
    <div class="toast-icon">
      <div class="toast"></div>
    </div>
    <div class="toast-title">
      {{ title }}
    </div>
  </div>
</template>
<script>
export default {
  name: 'the-toast',
  props: ['title'],
};
</script>
<style> </style>