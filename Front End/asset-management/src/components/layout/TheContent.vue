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
        <div class="toolbar-btn icon-box" @click="importFromExcelOnClick">
          <div class=""><i class="fa-solid fa-file-circle-plus"></i></div>
        </div>
        <div class="toolbar-btn icon-box" @click="exportToExcelOnClick">
          <div class="excel"></div>
        </div>
        <div class="toolbar-btn icon-box" @click="btnRemove">
          <div class="remove"></div>
        </div>
      </div>
    </div>

    <div class="m-grip">
      <!-- Table -->
      <div
        class="m-table-container"
        ref="MainTable"
        @scroll="
          this.$refs.FooterTable.scrollLeft = this.$refs.MainTable.scrollLeft
        "
      >
        <table class="m-table">
          <thead>
            <tr>
              <th style="padding-left: 16px; width: 50px">
                <BaseCheckbox></BaseCheckbox>
              </th>
              <th class="text-align-center w-50">STT</th>
              <th class="text-align-left w-130">Mã tài sản</th>
              <th class="text-align-left max-w-150">Tên tài sản</th>
              <th class="text-align-left max-w-150">Loại tài sản</th>
              <th class="text-align-left max-w-130">Bộ phận sử dụng</th>
              <th class="text-align-right w-60">Số lượng</th>
              <th class="text-align-right w-130">Nguyên giá</th>
              <th class="text-align-right w-130">HM/KH lũy kế</th>
              <th class="text-align-right w-130">Giá trị còn lại</th>
              <th class="text-align-center w-90">Chức năng</th>
            </tr>
          </thead>
          <div v-if="isLostConnection" class="table-msg">
            Không thể tải được dữ liệu
          </div>
          <BaseLoading v-else-if="isLoading"></BaseLoading>
          <div v-else-if="this.assetData.length == 0" class="table-msg">
            Không có dữ liệu
          </div>
          <tbody>
            <tr
              @dblclick="showEditDialog(asset)"
              v-for="(asset, index) in assetData"
              :key="index"
              :class="[{ 'm-tr-seleced': asset.checked }, 'm-tr']"
              @click="onRowClick(asset, $event)"
            >
              <td style="padding-left: 16px">
                <BaseCheckbox :checked="asset.checked"></BaseCheckbox>
              </td>
              <td class="text-align-center">{{ index + 1 }}</td>
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
                {{ currencyFormat(asset.Cost) }}
              </td>
              <td class="text-align-right">
                {{ currencyFormat(asset.Accumulated) }}
              </td>
              <td class="text-align-right">
                {{ currencyFormat(asset.Cost - asset.Accumulated) }}
              </td>
              <td>
                <div
                  class="m-function-box"
                  style="display: none; padding-left: 10px"
                >
                  <div class="icon-box-36 edit">
                    <div class="table-icon edit"></div>
                  </div>
                  <div class="icon-box-36 copy">
                    <div class="table-icon copy"></div>
                  </div>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>

      <!-- paging -->
      <div
        class="m-footer-container"
        ref="FooterTable"
        @scroll="
          this.$refs.MainTable.scrollLeft = this.$refs.FooterTable.scrollLeft
        "
      >
        <table class="m-table-footer">
          <tbody>
            <tr>
              <td style="min-width: 640px">
                <div class="m-paging-left">
                  <div class="m-total-number">
                    Tổng số:
                    <strong>{{ this.totalAssetListLength }}</strong> bản ghi
                  </div>
                  <BaseDropdown @onChose="getPageSize"></BaseDropdown>

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
                  ></BasePaginate>
                </div>
              </td>
              <td class="text-align-right w-60">
                {{ quantityTotal }}
              </td>
              <td class="text-align-right w-130">
                {{ currencyFormat(costTotal) }}
              </td>
              <td class="text-align-right w-130">
                {{ currencyFormat(accumulatedTotal) }}
              </td>
              <td class="text-align-right w-130">
                {{ currencyFormat(costTotal - accumulatedTotal) }}
              </td>
              <td class="w-90"></td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>

    <BaseDialog
      ref="dialog"
      :dialogTitle="isEditing ? 'Sửa sản phẩm' : 'Thêm sản phẩm'"
      :isEditing="isEditing"
      v-if="isDialogShow"
      :assetSelected="assetSelected"
      :departmentData="departmentData"
      :categoryData="categoryData"
      @filterAsset="filterAsset"
      @getAssetData="getAssetData"
      @dialogShow="dialogShow"
    ></BaseDialog>

    <BaseAlert
      v-if="alert.isShow"
      :alertType="alert.type"
      @closeAlert="this.alertShow(false)"
      @removeBtn="this.removeAsset"
    >
      <span v-if="isRemoveMulti && alert.type == 'remove'"
        ><strong>{{ alert.title }}</strong> tài sản đã được chọn. Bạn có muốn
        xóa các tài sản này khỏi danh sách?</span
      >
      <span v-else-if="alert.type == 'remove'"
        >Bạn có muốn xóa tài sản <strong>{{ alert.title }}</strong
        >?</span
      >

      <span v-else>Chọn ít nhất một tài sản để xóa </span>
    </BaseAlert>

    <BaseToast v-if="toast.isShow" :title="toast.title"> </BaseToast>

    <DragFolder
      v-if="isShowDragFolder"
      @isShowDlgImportFirstOnClick="isShowDlgImportFirstOnClick"
    />
  </div>
</template>
<script>
import axios from 'axios';
import DragFolder from '@/components/importingData/Import-Data-First.vue';
export default {
  components: {
    DragFolder,
  },
  async beforeMount() {
    this.pageSize = 20;
    // Lấy dữ liệu đã phân trang từ api
    await this.filterAsset();

    // Lấy tổng số bản ghi:
    await this.getAssetData();

    //Lấy dữ liệu Department

    try {
      const res = await axios.get('http://localhost:5290/api/v1/Departments');
      this.departmentData = res.data;
    } catch (error) {
      console.log(error);
    }

    // Lấy dữ liệu Category
    try {
      const res = await axios.get(
        'http://localhost:5290/api/v1/FixedAssetCategories'
      );
      this.categoryData = res.data;
    } catch (error) {
      console.log(error);
    }
  },

  computed: {
    // Tính tổng số trang
    totalPageIndex: function () {
      return Math.ceil(this.assetLength / this.pageSize);
    },

    // Tính tổng số lượng
    quantityTotal: function () {
      const quantityTotal = this.assetData.reduce((currentValue, item) => {
        return currentValue + Number(item.Quantity);
      }, 0);
      return quantityTotal;
    },

    //Tính tổng giá tiền
    costTotal: function () {
      const costTotal = this.assetData.reduce((currentValue, item) => {
        return currentValue + Number(item.Cost);
      }, 0);
      return costTotal;
    },

    // Tính giá trị lũy kế
    accumulatedTotal: function () {
      const accumulatedTotal = this.assetData.reduce((currentValue, item) => {
        return currentValue + Number(item.Accumulated);
      }, 0);
      return accumulatedTotal;
    },
  },

  methods: {
    isShowDlgImportFirstOnClick() {
      this.isShowDragFolder = false;
    },

    importFromExcelOnClick() {
      this.isShowDragFolder = true;
    },
    async exportToExcelOnClick(dataExportExcel) {
      // Nếu không chứa -> Xuất toàn bộ nguyên vật liệu có trong Database
      dataExportExcel = [];
      await this.handleExport(dataExportExcel);
    },

    async handleExport(dataExportExcel) {
      const tempDateTime = new Date();
      const fileName = `Tai-san${tempDateTime.getTime()}.xlsx`;
      await axios
        .post(
          'http://localhost:5290/api/v1/FixedAssets/Excel',
          dataExportExcel,
          {
            responseType: 'blob',
            contentType: 'application/json-patch+json',
          }
        )
        .then(function (res) {
          if (res) {
            var url = window.URL.createObjectURL(new Blob([res.data]));
            var a = document.createElement('a');
            a.href = url;
            a.download = fileName;
            document.body.appendChild(a);
            a.click();
            a.remove();
          }
        })
        .catch(function (res) {
          console.log(res);
        });
    },

    // Lấy số lượng tài sản xóa
    btnRemove() {
      // 1. Lọc danh sách những tàn sản đã chọn:
      this.checkedaAssetList = this.assetData.filter(
        (asset) => asset.checked == true
      );
      // 2. Hiển thị thông báo:
      // 2.1 Nếu chưa chọn tài sản nào:
      if (this.checkedaAssetList.length == 0) {
        this.alertShow(true, 'Bạn chưa chọn sản phẩm để xóa');
      } else if (this.checkedaAssetList.length == 1) {
        this.isRemoveMulti = false;

        this.alertShow(
          true,
          this.checkedaAssetList[0].FixedAssetCode,
          'remove'
        );
      } else {
        this.isRemoveMulti = true;
        // 2.2 Nếu đã chọn:
        var length = this.checkedaAssetList.length;
        // hiển thị title cảnh báo
        var title = length < 10 ? `0${length}` : length;
        this.alertShow(true, title, 'remove');
      }
    },

    async removeAsset() {
      // Khởi tạo mảng id cần xóa
      var idList = [];
      // Thêm id vào mảng
      this.checkedaAssetList.forEach((element) => {
        idList.push(element.FixedAssetId);
      });
      // Gửi lên API
      try {
        const res = await axios.delete(
          `http://localhost:5290/api/v1/FixedAssets/DeleteMulti`,
          {
            data: JSON.stringify(idList),
            headers: {
              'content-type': 'application/json',
            },
          }
        );
        console.log(res.data);
        // Nếu trả về 1 object => có lỗi tài sản đã có chứng từ không xóa được
        if (typeof res.data == 'object') {
          this.alertShow(true, res.data);
        }
        // Không có lỗi thì load lại bảng
        else {
          // Load lại bảng
          this.filterAsset();

          // Cập nhật lại tổng bản ghi
          this.getAssetData();

          //  Hiển thị toast xóa thành công
          this.toastShow('Xóa dữ liệu thành công');
          // gán lại giá trị cho list tạm thời về rỗng
          this.checkedaAssetList = [];
          // Tắt alert
          this.alertShow(false);
        }
      } catch (error) {
        console.log(error);
      }
    },

    alertShow(alert, title, type) {
      this.alert.isShow = alert;
      this.alert.title = title;
      this.alert.type = type;
    },

    // Hiển thị dialog
    async showEditDialog(asset) {
      // clearTimeout(this.clickTimeout);
      this.isEditing = true;
      await this.getAssetById(asset.FixedAssetId);
      //Hiểm thị form
      this.dialogShow(true);
    },

    // Lấy dữ liệu theo id
    async getAssetById(assetId) {
      this.isDialogLoading = true;
      try {
        const res = await axios.get(
          `http://localhost:5290/api/v1/FixedAssets/${assetId}`
        );
        this.assetSelected = res.data;
      } catch (error) {
        console.error(error);
      }
    },

    // gán dữ liệu pageSize từ dropdown
    async getPageSize(option) {
      this.pageSize = option;
      this.pageIndex = 1;
      await this.filterAsset();
    },

    // Lấy thông tin trang từ paging
    async getPageIndex(pageNum) {
      this.pageIndex = pageNum;
      // thực hiện filter theo pageIndex
      await this.filterAsset();
    },

    // Lấy tổng bản ghi:
    async getAssetData() {
      // this.isLoading = true;
      try {
        const res = await axios.get('http://localhost:5290/api/v1/FixedAssets');
        this.totalAssetListLength = res.data.length;
      } catch (error) {
        console.log(error);
      }
    },

    // Lấy danh sách đã phân trang
    async filterAsset() {
      this.isLostConnection = false;
      this.isLoading = true;
      try {
        const res = await axios.get(
          'http://localhost:5290/api/v1/FixedAssets/Filter',
          {
            params: {
              FixedAssetFilter: this.searchBox,
              FixedAssetCategoryName: this.searchCategory,
              DepartmentName: this.searchDepartment,
              pageIndex: this.pageIndex,
              pageSize: this.pageSize,
            },
          }
        );
        // Thêm trường checkd vào obj
        res.data.FilterList.map((element) => {
          element.checked = false;
        });
        this.assetData = res.data.FilterList;
        this.assetLength = res.data.FilterCount;
        this.isLoading = false;
      } catch (error) {
        this.isLostConnection = true;
        this.isLoading = false;
      }
    },

    // click vào 1 dòng
    onRowClick(asset, $event) {
      //Nếu ấn vào edit
      if ($event.target.classList.contains('edit')) {
        this.showEditDialog(asset);
      }
      // Nếu ấn vào copy
      else if ($event.target.classList.contains('copy')) {
        console.log('second');
        // this.showCloneDialog(asset.FixedAssetId);
      }
      // Nếu ấn vào cả dòng
      else {
        // Kiểm tra xem có check hết không:
        // this.checkedAll =
        //   this.checkedaAssetList.length != this.assetData.length ? false : true;
        // Tích vào ô cần tích:
        asset.checked = !asset.checked;
        // if (asset.checked == true) {
        //   asset.checked = false;
        // } else {
        //   asset.checked = true;
        // }
      }
    },

    // Format tiền
    currencyFormat(value) {
      var format = `${value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, '.')}`;
      return format;
    },

    //Hiển thị thông báo
    toastShow(title) {
      this.toast.isShow = true;
      this.toast.title = title;
      setTimeout(() => {
        this.toast.isShow = false;
      }, 3000);
    },

    dialogShow(isShow) {
      this.isDialogShow = isShow;
    },
  },
  data() {
    return {
      isLostConnection: false,
      alert: {
        title: '',
        isShow: false,
        type: '',
      },
      toast: {
        title: '',
        isShow: false,
      },
      isShowDragFolder: false,
      departmentData: [],
      categoryData: [],
      isDialogShow: false,
      assetData: [],
      totalAssetListLength: 0,
      assetLength: null,
      searchCategory: null,
      searchDepartment: null,
      searchBox: null,
      pageIndex: null,
      pageSize: null,
    };
  },
};
</script>
<style scoped>
@import url('@/assets/font-awesome/css/all.min.css');
</style>
