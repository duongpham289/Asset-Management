<template>
  <div v-on:clickout="tab" class="m-combobox" ref="combobox">
    <div class="combobox-contaner">
      <div v-if="hasIcon" class="combobox-icon">
        <div class="filter"></div>
      </div>
      <input
        type="text"
        ref="input"
        :required="required"
        :class="!hasIcon ? 'input-no-icon' : 'input-icon'"
        :name="name"
        :title="title"
        :maxlength="maxlength"
        :placeholder="placeholder"
        @click="onClick"
        @keydown.tab="tab"
        @focus="setFocus"
        @blur="outFocus"
        @keydown.up="up"
        @keydown.down="down"
        @input="onChangeHandler"
        @keydown.enter="selectItem"
        :value="this.modelValue"
      />

      <div
        style="position: absolute; right: 0"
        @click="toggleOptionList()"
        class="icon-combobox"
      >
        <div v-if="isOptionShow" class="up"></div>
        <div v-else class="down"></div>
      </div>
    </div>
    <ul class="m-option-list" v-if="isOptionShow" ref="optionList">
      <li
        v-for="(option, index) in matches"
        :key="index"
        class="m-option-item"
        @click="choseOption(index, option)"
        :class="{ 'm-item-selected': this.selecedIndex == index }"
      >
        <div class="item-text-limit">
          {{ option[this.filterby] }}
        </div>
      </li>
    </ul>
  </div>
</template>
<script>
import "clickout-event";
export default {
  name: "the-combobox",
  emits: ["blur", "keydown", "update:modelValue", "selectItem"],

  props: [
    "hasIcon",
    "placeholder",
    "filterby",
    "optionList",
    "modelValue",
    "name",
    "required",
    "title",
    "maxlength",
  ],

  watch: {
    /**
     * Mô tả : Khi nào đóng optionlist thì validate
     * @param
     * @return
     * Created by: Lê Thiện Tuấn - MF1118
     * Created date: 23:56 28/05/2022
     */
    isFocus: function (newValue) {
      if (newValue == false) {
        this.$refs.input.classList.remove("input-focus");

        this.validateRequired();
      } else {
        this.$refs.input.classList.add("input-focus");
      }
    },

    selecedIndex: function (newValue, oldValue) {
      if (newValue != oldValue) {
        this.scrollToItem();
      }
    },

    // Theo dõi giá trị mới của input để hiển thị optionList
    modelValue: function (newValue) {
      // Hiển thị option list:
      this.isOptionShow = true;
      // Gán index về 0:
      this.selecedIndex = 0;

      if (newValue == undefined || newValue == "") {
        this.matches = this.optionList;
      } else {
        this.matches = this.optionList.filter((item) =>
          item[this.filterby]
            .toLowerCase()
            .includes(this.modelValue.toLowerCase())
        );
      }
    },
  },

  methods: {
    /**
     * Mô tả : chọn ô input thì bôi đen chữ
     * @param
     * @return
     * Created by: Lê Thiện Tuấn - MF1118
     * Created date: 20:59 30/05/2022
     */
    onClick() {
      this.isToggle = false;
      this.setFocus();
    },
    /**
     * Mô tả : Lấy giá trị từ v-model bên cha
     * @param
     * @return
     * Created by: Lê Thiện Tuấn - MF1118
     * Created date: 00:00 02/05/2022
     */
    onChangeHandler(e) {
      e.preventDefault();
      //gán lại giá trị
      this.$emit("update:modelValue", e.target.value);
    },

    /**
     * Mô tả : khi focus vào thì hiện out line, blur thì bỏ outline
     * @param
     * @return
     * Created by: Lê Thiện Tuấn - MF1118
     * Created date: 11:23 30/04/2022
     */
    setFocus() {
      this.isFocus = true;
      // Nếu chưa nhập gì thì matches list hiển thị tất cả
      if (this.$refs.input.value == null || this.$refs.input.value == "") {
        this.matches = this.optionList;
      }

      if (this.isToggle == true) {
        // Hiển thị option List:
        this.isOptionShow = !this.isOptionShow;
      } else {
        this.isOptionShow = true;
      }

      // Hiển thị theo vị trí của index được chọn:
      this.scrollToItem();

      // Bôi đen tất cả text
      this.$refs.input.select();
    },

    // outFocus() {
    //   this.isFocus = false;
    // },

    /**
     * Mô tả : Validate required
     * @param
     * @return
     * Created by: Lê Thiện Tuấn - MF1118
     * Created date: 16:54 06/05/2022
     */
    validateRequired() {
      if (
        this.required &&
        (this.modelValue === undefined || this.modelValue.trim() === "")
      ) {
        this.$refs.input.classList.add("m-input-error");
      } else {
        this.$refs.input.classList.remove("m-input-error");
      }
    },

    /**
    //  * Mô tả : clear input
    //  * @param
    //  * @return
    //  * Created by: Lê Thiện Tuấn - MF1118
    //  * Created date: 14:52 28/04/2022
    //  */
    // clearInput() {
    //   this.$emit("update:modelValue");
    //   this.isOptionShow = false;
    //   // this.validateRequired();
    //   this.selecedIndex = 0;
    // },

    /**
     * Mô tả : xử lí sự kiện onClick vào optionList
     * @param
     * @return
     * Created by: Lê Thiện Tuấn - MF1118
     * Created date: 15:31 28/04/2022
     */
    choseOption(index) {
      this.selecedIndex = index;
      this.selectItem();
      this.toggleOptionList();
    },

    /**
     * Mô tả : Chọn item
     * @param
     * @return
     * Created by: Lê Thiện Tuấn - MF1118
     * Created date: 12:32 22/05/2022
     */
    async selectItem() {
      // 1. Chọn theo index của matches list:
      // 1.1 Lấy obj đã ch gán vào selectedItem
      this.selecedItem = this.matches[this.selecedIndex];
      // 1.2 Cập nhật giá trị vào input
      await this.$emit(
        "update:modelValue",
        this.matches[this.selecedIndex][this.filterby]
      );

      // 2. Tìm lại index của giá trị đã chọn theo List đầy đủ:
      // 2.1 Tìm index của selectItem trong mảng ban đầu ( không phải mảng matches):
      this.selecedIndex = this.optionList.findIndex((object) => {
        return object === this.selecedItem;
      });
      // 2.2 Gán lại matches list thành optionList:
      this.matches = [...this.optionList];

      //  truyền cả obj lên cho component cha:
      this.$emit("selectItem", this.selecedItem);
      // Validate lại dữ liệu:
      this.isOptionShow = false;
      // Bôi đen chữ
      this.$refs.input.select();
    },

    /**
     * Mô tả : Ấn nút lên thay đổi index của option
     * @param
     * @return
     * Created by: Lê Thiện Tuấn - MF1118
     * Created date: 22:47 28/05/2022
     */
    up() {
      // Nếu index bằng không thì không ấn được nữa
      if (this.selecedIndex == 0) {
        this.selecedIndex = this.matches.length - 1;
        return;
      }
      this.selecedIndex -= 1;
    },

    /**
     * Mô tả :  Ấn nút xuống thay đổi index của option
     * @param
     * @return
     * Created by: Lê Thiện Tuấn - MF1118
     * Created date: 22:48 28/05/2022
     */
    down() {
      if (this.selecedIndex >= this.matches.length - 1) {
        this.selecedIndex = 0;
        return;
      }
      this.selecedIndex += 1;
    },

    /**
     * Mô tả : Ấn tab thì đóng optionList
     * @param
     * @return
     * Created by: Lê Thiện Tuấn - MF1118
     * Created date: 22:48 28/05/2022
     */
    tab() {
      this.isOptionShow = false;
      this.isFocus = false;
    },

    /**
     * Mô tả : Ấn vào nút mũi tên hiển thị/ tắt optionList
     * @param
     * @return
     * Created by: Lê Thiện Tuấn - MF1118
     * Created date: 10:21 08/05/2022
     */
    toggleOptionList() {
      this.isToggle = true;
      // this.isOptionShow = false;
      this.$refs.input.focus();
    },

    /**
     * Mô tả : Hiển thị theo index của selected item
     * @param
     * @return
     * Created by: Lê Thiện Tuấn - MF1118
     * Created date: 22:49 28/05/2022
     */
    scrollToItem() {
      this.$nextTick(() => {
        if (this.isOptionShow == true) {
          this.$refs.optionList.scrollTop = this.selecedIndex * 36;
        }
      });
    },
  },

  data() {
    return {
      selecedIndex: 0,
      matches: [],
      selecedItem: null,
      isFocus: false,
      isOptionShow: false,
      isToggle: false,
    };
  },
};
</script>
<style></style>
