<template>
  <div class="m-dialog" v-shortkey="['ctrl', 's']" @shortkey="onSubmit()">
    <div class="m-modal">
      <div class="m-nodal-title">{{ dialogTitle }}</div>
      <div class="m-modal-close" @click="onCancel">
        <div class="close"></div>
      </div>

      <form class="m-modal-centent" ref="form" autocomplete="off">
        <div class="modal-row">
          <div class="modal-field">
            <label>Mã tài sản <span>*</span></label>
            <BaseInput
              :required="true"
              ref="FixedAssetCode"
              type="text"
              maxlength="20"
              placeholder="Nhập mã tài sản"
              v-model="asset.FixedAssetCode"
              name="Mã tài sản"
            ></BaseInput>
          </div>
          <div class="modal-field modal-field-long">
            <label for="input">Tên tài sản <span>*</span></label>
            <BaseInput
              :required="true"
              ref="FixedAssetName"
              maxlength="255"
              :name="'Tên tài sản'"
              placeholder="Nhập tên tài sản"
              v-model="asset.FixedAssetName"
            >
            </BaseInput>
          </div>
        </div>

        <div class="modal-row">
          <div class="modal-field">
            <label for="input">Mã bộ phận sử dụng <span>*</span></label>

            <BaseCombobox
              :required="true"
              :maxlength="50"
              :name="'Mã bộ phận sử dụng'"
              ref="DepartmentCode"
              placeholder="Chọm mã bộ phận sử dụng"
              :optionList="departmentData"
              filterby="DepartmentCode"
              v-model="asset.DepartmentCode"
            ></BaseCombobox>
          </div>
          <div class="modal-field modal-field-long">
            <label for="input">Tên bộ phận sử dụng</label>
            <BaseInput disabled v-model="asset.DepartmentName"> </BaseInput>
          </div>
        </div>

        <div class="modal-row">
          <div class="modal-field">
            <label for="input">Mã loại tài sản <span>*</span></label>
            <BaseCombobox
              :maxlength="50"
              required="true"
              :optionList="categoryData"
              ref="FixedAssetCategoryCode"
              name="Mã loại tài sản"
              filterby="FixedAssetCategoryCode"
              placeholder="Chọm mã loại tài sản"
              @selectItem="autoFieldCombobox"
              v-model="asset.FixedAssetCategoryCode"
            ></BaseCombobox>
          </div>
          <div class="modal-field modal-field-long">
            <label for="input">Tên loại tài sản</label>
            <BaseInput
              disabled
              v-model="asset.FixedAssetCategoryName"
            ></BaseInput>
          </div>
        </div>

        <div class="modal-row">
          <div class="modal-field">
            <label for="input">Số lượng<span> *</span></label>
            <BaseInput
              :required="true"
              :isNumber="true"
              name="Số lượng"
              maxlength="11"
              ref="Quantity"
              classParent="number-input-icon"
              v-model="asset.Quantity"
              @keydown.down="
                asset.Quantity < 1 ? (asset.Quantity = 0) : asset.Quantity--
              "
              @keydown.up="asset.Quantity++"
            ></BaseInput>
            <div class="spin-button-container">
              <div class="up" @click="asset.Quantity++"></div>
              <div
                class="down"
                @click="
                  asset.Quantity < 1 ? (asset.Quantity = 0) : asset.Quantity--
                "
              ></div>
            </div>
          </div>
          <div class="modal-field">
            <label for="input">Nguyên giá <span>*</span></label>
            <BaseInput
              maxlength="25"
              :required="true"
              :isNumber="true"
              ref="Cost"
              name="Nguyên giá"
              classParent="number-input"
              v-model="Cost"
            ></BaseInput>
          </div>
          <div class="modal-field">
            <label for="input">Số năm sử dụng <span>*</span></label>
            <BaseInput
              ref="LifeTime"
              maxlength="11"
              :required="true"
              :isNumber="true"
              name="Số năm sử dụng"
              classParent="number-input"
              v-model="asset.LifeTime"
            ></BaseInput>
          </div>
        </div>

        <div class="modal-row">
          <div class="modal-field">
            <label for="input">Tỉ lệ hao mòn(%) <span>*</span></label>
            <BaseInput
              ref="DepreciationRate"
              maxlength="11"
              :isNumber="true"
              type="number"
              min="0"
              required="true"
              name="Tỉ lệ hao mòn"
              classParent="number-input-icon"
              v-model="DepreciationRate"
            ></BaseInput>

            <div class="spin-button-container">
              <div class="up" @click="DepreciationRate++"></div>
              <div
                class="down"
                @click="
                  DepreciationRate < 1
                    ? (DepreciationRate = 0)
                    : DepreciationRate--
                "
              ></div>
            </div>
          </div>

          <div class="modal-field">
            <label for="input">Giá trị hao mòm năm <span>*</span></label>
            <BaseInput
              ref="DepreciationValue"
              :required="true"
              :isNumber="true"
              type="text"
              maxlength="25"
              name="Giá trị hao mòm năm"
              classParent="number-input"
              v-model="DepreciationValue"
            ></BaseInput>
          </div>
          <div class="modal-field">
            <label for="input">Năm theo dõi</label>
            <BaseInput
              classParent="number-input"
              disabled
              v-model="asset.TrackedYear"
            ></BaseInput>
          </div>
        </div>

        <div class="modal-row">
          <div class="modal-field">
            <label for="input">Ngày mua <span>*</span></label>
            <Datepicker
              :required="true"
              name="Ngày mua"
              ref="purchaseDate"
              v-model="asset.PurchaseDate"
            ></Datepicker>
          </div>
          <div class="modal-field">
            <label for="input">Ngày bắt đầy sử dụng <span>*</span></label>
            <Datepicker
              :required="true"
              name="Ngày bắt đầu sử dụng"
              ref="UseDate"
              v-model="asset.UseDate"
            ></Datepicker>
          </div>
        </div>
      </form>

      <div class="m-modal-footer">
        <BaseButton
          style="border: none"
          type="outline-button"
          @click="onCancel"
          buttonTitle="Hủy"
        ></BaseButton>
        <BaseButton @click="onSubmit($event)" buttonTitle="Lưu"></BaseButton>
      </div>
    </div>
  </div>
</template>
<script>
import axios from "axios";
import {
  cancel_msg,
  error_msg,
  toast_msg,
} from "../../assets/resource/ResourceMsg";
export default {
  name: "base-dialog",
  props: [
    "assetSelected",
    "dialogTitle",
    "isEditing",
    "departmentData",
    "categoryData",
  ],

  beforeMount() {
    // Gắn dữ liệu:
    this.asset = this.assetSelected;

    // Gán giá trị cho trường Năm theo dõi:
    this.asset.TrackedYear = new Date().getFullYear();
  },

  mounted() {
    // Focus vào ô input đầu
    this.$refs.FixedAssetCode.setFocus();

    // Tạo ra obj đầu vào để so sánh
    this.assetCopy = Object.assign({}, this.asset);
  },
  computed: {
    /**
     * Mô tả : Tính giá trị hao mòn năm
     * @param
     * @return
     * Created by: Bùi Đức Anh
     */
    DepreciationValue: {
      get() {
        return this.formatSalary(this.asset.DepreciationValue.toString());
      },
      set(newValue) {
        newValue = newValue.replaceAll(".", "");
        this.asset.DepreciationValue = newValue;
      },
    },

    /**
     * Mô tả : format tiền (nguyên giá)
     * @param
     * @return
     * Created by: Bùi Đức Anh
     */
    Cost: {
      get() {
        return this.formatSalary(this.asset.Cost.toString());
      },
      set(newValue) {
        newValue = newValue.replaceAll(".", "");
        this.asset.Cost = newValue;
        // Tính giá trị hao mòn năm
        this.asset.DepreciationValue = Math.floor(
          this.asset.Cost * this.asset.DepreciationRate
        );
      },
    },

    /**
     * Mô tả : Hiển thị tỉ lệ hao mòm
     * @param
     * @return
     * Created by: Bùi Đức Anh
     */
    DepreciationRate: {
      get() {
        return this.asset.DepreciationRate * 100;
      },
      set(newValue) {
        this.asset.DepreciationRate = newValue / 100;
        this.asset.DepreciationValue =
          this.asset.DepreciationRate * this.asset.Cost;
      },
    },
  },

  watch: {
    "this.asset.PurchaseDate"(newValue) {
      console.log(newValue);
    },
    /**
     * Mô tả : Theo dõi sự thay đổi của mã loại tài sản để gán dữ liệu cho các trường input
     * @param
     * @return
     * Created by: Bùi Đức Anh
     */
    "asset.FixedAssetCategoryCode"(newValue) {
      var data = this.categoryData.find(
        (item) => item.FixedAssetCategoryCode == newValue
      );
      if (!data) {
        this.asset.FixedAssetCategoryName = "";
        this.asset.DepreciationRate = "";
        this.asset.LifeTime = "";
      }
    },
    /**
     * Mô tả : Theo dõi sự thay đổi của mã bộ phận sử dụng để gán dữ liệu cho các trường input
     * @param
     * @return
     * Created by: Bùi Đức Anh
     */
    "asset.DepartmentCode"(newValue) {
      var data = this.departmentData.find(
        (item) => item.DepartmentCode == newValue
      );
      if (data) {
        this.asset.DepartmentName = data.DepartmentName;
      } else {
        this.asset.DepartmentName = "";
      }
    },
  },

  methods: {
    /**
     * Mô tả : format tiền
     * @param
     * @return
     * Created by: Bùi Đức Anh
     */
    formatSalary(value) {
      var format = `${value.replace(/\B(?=(\d{3})+(?!\d))/g, ".")}`;
      return format;
    },

    /**
     * Mô tả : Tính Giá trị hao mòn khi chọn combobox
     * @param
     * @return
     * Created by: Bùi Đức Anh
     */
    autoFieldCombobox(option) {
      this.asset.DepreciationRate = option.DepreciationRate;
      this.asset.FixedAssetCategoryName = option.FixedAssetCategoryName;
      this.asset.LifeTime = option.LifeTime;
      // Tính giá trị hao mòn năm:
      this.asset.DepreciationValue =
        this.asset.Cost * this.asset.DepreciationRate;
    },

    /**
     * Mô tả : Tính những giá trị còn lại để đẩy lên api
     * @param
     * @return
     * Created by: Bùi Đức Anh
     */
    autoFieldData() {
      // Ngày tạo
      if (!this.isEditing) {
        this.asset.CreatedDate = new Date();
      }
      // Ngày sứa
      this.asset.ModifiedDate = new Date();
      // Tính Năm sử dụng(Năm hiện tại - năm ngày bắt đầu sử dụng)
      this.asset.ProductionYear =
        this.asset.TrackedYear - new Date(this.asset.UseDate).getFullYear();
      // Tính Lũy kế
      this.asset.Accumulated =
        this.asset.Cost *
        this.asset.DepreciationRate *
        this.asset.ProductionYear;
    },
  },
  data() {
    return {
      assetCopy: {},
      asset: {},
      errorList: [],
      firstEmptyElement: null,
    };
  },
};
</script>
<style></style>
