<template>
  <div class="m-content">
    <div class="m-toolbar">
      <div class="m-toolbar-left">
        <div class="toolbar-field search-field">
          <input
            placeholder="Tìm kiếm tài sản"
            ref="searchInput"
            class="m-search"
            @input="searchInput"
          />
          <div class="search-icon">
            <div class="search"></div>
          </div>
        </div>
        <div class="toolbar-field combobox-field">
          <!-- <BaseCombobox></BaseCombobox> -->
          <BaseInput class="m-input"></BaseInput>
        </div>

        <div class="toolbar-field combobox-field">
          <BaseInput class="m-input"></BaseInput>
          <!-- <BaseCombobox></BaseCombobox> -->
        </div>
      </div>
      <div class="m-toolbar-right">
        <BaseButton
          style="box-shadow: 0 2px 6px rgba(0, 0, 0, 0.16)"
          buttonTitle="+Thêm tài sản"
        ></BaseButton>
        <div class="toolbar-btn icon-box">
          <div class="excel"></div>
        </div>
        <div class="toolbar-btn icon-box">
          <div class="remove"></div>
        </div>
      </div>
    </div>

    <div class="m-grip">
      <!-- Table -->
      <div class="m-table-container">
        <table class="m-table">
          <thead>
            <tr>
              <th style="padding-left: 16px; width: 50px">
                <BaseCheckbox></BaseCheckbox>
              </th>
              <th class="text-align-left" style="width: 50px">STT</th>
              <th class="text-align-left">Mã tài sản</th>
              <th class="text-align-left">Tên tài sản</th>
              <th class="text-align-left">Loại tài sản</th>
              <th class="text-align-left">Bộ phận sử dụng</th>
              <th class="text-align-right" style="width: 60px">Số lượng</th>
              <th class="text-align-right" style="width: 130px">Nguyên giá</th>
              <th class="text-align-right" style="width: 130px">
                HM/KH lũy kế
              </th>
              <th class="text-align-right" style="width: 130px">
                Giá trị còn lại
              </th>
              <th class="text-align-center" style="width: 90px">Chức năng</th>
            </tr>
          </thead>
          <!-- <div v-if="isLostConnection" class="table-msg">
            Không thể tải được dữ liệu
          </div> -->
          <!-- <BaseLoading v-else-if="isLoading"></BaseLoading> -->
          <!-- <div v-else-if="this.assetData.length == 0" class="table-msg">
            Không có dữ liệu
          </div> -->
          <tbody>
            <!-- @dblclick="showEditDialog(asset)"
              @click="onRowClick(asset, $event)" -->
            <tr v-for="(asset, index) in assetData" :key="index" class="m-tr">
              <td style="padding-left: 16px">
                <!-- :checked="checkedaAssetList.includes(asset)" -->
                <BaseCheckbox></BaseCheckbox>
              </td>
              <td class="text-align-left">{{ index + 1 }}</td>
              <td class="text-align-left max-w-130">
                {{ asset.FixedAssetCode }}
              </td>
              <td
                class="text-align-left max-w-150"
                :title="asset.FixedAssetName"
              >
                {{ asset.FixedAssetName }}
              </td>
              <td
                class="text-align-left max-w-150"
                :title="asset.FixedAssetCategoryName"
              >
                {{ asset.FixedAssetCategoryName }}
              </td>
              <td
                class="text-align-left max-w-130"
                :title="asset.DepartmentName"
              >
                {{ asset.DepartmentName }}
              </td>
              <td class="text-align-right">{{ asset.Quantity }}</td>
              <td class="text-align-right">
                {{ asset.Cost }}
              </td>
              <td class="text-align-right">
                {{ asset.Accumulated }}
              </td>
              <td class="text-align-right">
                {{ asset.Cost - asset.Accumulated }}
              </td>
              <td>
                <div class="m-function-box" style="display: none">
                  <div class="icon-box edit">
                    <div class="table-icon edit"></div>
                  </div>
                  <div class="icon-box copy">
                    <div class="table-icon copy"></div>
                  </div>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>

      <!-- paging -->
      <table class="m-table-footer">
        <tbody>
          <tr>
            <td>
              <div class="m-paging-left">
                <div class="m-total-number">
                  Tổng số: <strong> 30 </strong> bản ghi
                </div>
                <!-- <BaseDropdown
                  :defaultValue="this.pageSize"
                  @onChose="getPageSize"
                ></BaseDropdown>
                <BasePaginate
                  v-model="pageIndex"
                  :pageCount="totalPageIndex"
                  :prev-text="'pre'"
                  :prev-link-class="'prev-link-class'"
                  :next-text="'next'"
                  :next-link-class="'next-link-class'"
                  :container-class="'m-paging-list'"
                  :prev-class="'prev-class'"
                  :click-handler="getPageIndex"
                ></BasePaginate> -->
              </div>
            </td>
            <td class="text-align-right" style="width: 60px">quantityTotal</td>
            <td class="text-align-right" style="width: 130px">costTotal</td>
            <td class="text-align-right" style="width: 130px">
              accumulatedTotal
            </td>
            <td class="text-align-right" style="width: 130px">
              costTotal - accumulatedTotal
            </td>
            <td style="width: 90px"></td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {
      assetData: {},
    };
  },
};
</script>
<style></style>
