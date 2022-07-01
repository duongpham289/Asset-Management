<template>
  <div
    class="m-dropdown"
    v-on:clickout="this.isOptionShow = false"
    ref="dropdown"
  >
    <div class="m-dropdown-main" @click.prevent="onClick">
      <div class="dropdown-title">{{ optionChose }}</div>
      <div class="dropdown-icon">
        <div :class="[this.isOptionShow ? 'up' : 'down']"></div>
      </div>
    </div>
    <Teleport to="body">
      <ul
        v-if="isOptionShow"
        class="m-dropdown-option"
        :style="{
          top: this.optionPos.top + 'px',
          left: this.optionPos.left + 'px',
          width: this.optionPos.width + 'px',
        }"
      >
        <li
          v-for="(option, index) in optionList"
          :key="index"
          @click="onChose(option)"
          class="option-item"
          :class="[this.optionChose == option ? 'option-item-seleced ' : '']"
        >
          {{ option }}
        </li>
      </ul>
    </Teleport>
  </div>
</template>
<script>
export default {
  name: 'base-dropdown',
  beforeMount() {
    // Gán giá trị ban đầu (Giá trị đầu tiên trong danh sách lựa chọn)
    this.optionChose = this.optionList[0];
  },

  /**
   * Mô tả : Xử lí khi thay đỏi window size
   * @param
   * @return
   * Created by: Lê Thiện Tuấn - MF1118
   * Created date: 23:04 11/06/2022
   */
  created() {
    window.addEventListener('resize', this.resizeHandler);
  },

  methods: {
    /**
     * Mô tả : Lấy lại giá trị vị trí cho option list
     * @param
     * @return
     * Created by: Lê Thiện Tuấn - MF1118
     * Created date: 00:36 12/06/2022
     */
    resizeHandler() {
      if (this.isOptionShow == true) {
        this.dropdownPos = this.$refs.dropdown.getBoundingClientRect();
        this.setDropDownPos();
      }
    },

    /**
     * Mô tả : Xử lí mở optionlist
     * @param
     * @return
     * Created by: Lê Thiện Tuấn - MF1118
     * Created date: 22:41 11/06/2022
     */
    setDropDownPos() {
      this.optionPos = {
        top: this.dropdownPos.top - 100,
        left: this.dropdownPos.left,
        width: this.dropdownPos.width,
      };
    },

    /**
     * Mô tả : xử lí click dropdown
     * @param
     * @return
     * Created by: Lê Thiện Tuấn - MF1118
     * Created date: 22:44 11/06/2022
     */
    onClick() {
      this.isOptionShow = !this.isOptionShow;
      if (this.isOptionShow == true) {
        this.dropdownPos = this.$refs.dropdown.getBoundingClientRect();
        this.setDropDownPos();
      }
    },

    onChose(option) {
      this.optionChose = option;
      this.isOptionShow = false;
      this.$emit('onChose', option);
    },
  },
  data() {
    return {
      optionList: [20, 30, 40, 100],
      isOptionShow: false,
      optionChose: '',
      optionPos: {},
      dropdownPos: {},
    };
  },
};
</script>
<style></style>
