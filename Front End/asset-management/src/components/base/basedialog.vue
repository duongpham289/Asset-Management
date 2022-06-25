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
            <MISAInput
              :required="true"
              ref="FixedAssetCode"
              type="text"
              maxlength="20"
              placeholder="Nhập mã tài sản"
              v-model="asset.FixedAssetCode"
              name="Mã tài sản"
            ></MISAInput>
          </div>
          <div class="modal-field modal-field-long">
            <label for="input">Tên tài sản <span>*</span></label>
            <MISAInput
              :required="true"
              ref="FixedAssetName"
              maxlength="255"
              :name="'Tên tài sản'"
              placeholder="Nhập tên tài sản"
              v-model="asset.FixedAssetName"
            >
            </MISAInput>
          </div>
        </div>

        <div class="modal-row">
          <div class="modal-field">
            <label for="input">Mã bộ phận sử dụng <span>*</span></label>

            <MISACombobox
              :required="true"
              :maxlength="50"
              :name="'Mã bộ phận sử dụng'"
              ref="DepartmentCode"
              placeholder="Chọm mã bộ phận sử dụng"
              :optionList="departmentData"
              filterby="DepartmentCode"
              v-model="asset.DepartmentCode"
            ></MISACombobox>
          </div>
          <div class="modal-field modal-field-long">
            <label for="input">Tên bộ phận sử dụng</label>
            <MISAInput disabled v-model="asset.DepartmentName"> </MISAInput>
          </div>
        </div>

        <div class="modal-row">
          <div class="modal-field">
            <label for="input">Mã loại tài sản <span>*</span></label>
            <MISACombobox
              :maxlength="50"
              required="true"
              :optionList="categoryData"
              ref="FixedAssetCategoryCode"
              name="Mã loại tài sản"
              filterby="FixedAssetCategoryCode"
              placeholder="Chọm mã loại tài sản"
              @selectItem="autoFieldCombobox"
              v-model="asset.FixedAssetCategoryCode"
            ></MISACombobox>
          </div>
          <div class="modal-field modal-field-long">
            <label for="input">Tên loại tài sản</label>
            <MISAInput
              disabled
              v-model="asset.FixedAssetCategoryName"
            ></MISAInput>
          </div>
        </div>

        <div class="modal-row">
          <div class="modal-field">
            <label for="input">Số lượng<span> *</span></label>
            <MISAInput
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
            ></MISAInput>
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
            <MISAInput
              maxlength="25"
              :required="true"
              :isNumber="true"
              ref="Cost"
              name="Nguyên giá"
              classParent="number-input"
              v-model="Cost"
            ></MISAInput>
          </div>
          <div class="modal-field">
            <label for="input">Số năm sử dụng <span>*</span></label>
            <MISAInput
              ref="LifeTime"
              maxlength="11"
              :required="true"
              :isNumber="true"
              name="Số năm sử dụng"
              classParent="number-input"
              v-model="asset.LifeTime"
            ></MISAInput>
          </div>
        </div>

        <div class="modal-row">
          <div class="modal-field">
            <label for="input">Tỉ lệ hao mòn(%) <span>*</span></label>
            <MISAInput
              ref="DepreciationRate"
              maxlength="11"
              :isNumber="true"
              type="number"
              min="0"
              required="true"
              name="Tỉ lệ hao mòn"
              classParent="number-input-icon"
              v-model="DepreciationRate"
            ></MISAInput>

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
            <MISAInput
              ref="DepreciationValue"
              :required="true"
              :isNumber="true"
              type="text"
              maxlength="25"
              name="Giá trị hao mòm năm"
              classParent="number-input"
              v-model="DepreciationValue"
            ></MISAInput>
          </div>
          <div class="modal-field">
            <label for="input">Năm theo dõi</label>
            <MISAInput
              classParent="number-input"
              disabled
              v-model="asset.TrackedYear"
            ></MISAInput>
          </div>
        </div>

        <div class="modal-row">
          <div class="modal-field">
            <label for="input">Ngày mua <span>*</span></label>
            <MISADatepicker
              :required="true"
              name="Ngày mua"
              ref="purchaseDate"
              v-model="asset.PurchaseDate"
            ></MISADatepicker>
          </div>
          <div class="modal-field">
            <label for="input">Ngày bắt đầy sử dụng <span>*</span></label>
            <MISADatepicker
              :required="true"
              name="Ngày bắt đầu sử dụng"
              ref="UseDate"
              v-model="asset.UseDate"
            ></MISADatepicker>
          </div>
        </div>
      </form>

      <div class="m-modal-footer">
        <MISAButton
          style="border: none"
          type="outline-button"
          @click="onCancel"
          buttonTitle="Hủy"
        ></MISAButton>
        <MISAButton @click="onSubmit($event)" buttonTitle="Lưu"></MISAButton>
      </div>
    </div>
  </div>
</template>
<script></script>
<style></style>