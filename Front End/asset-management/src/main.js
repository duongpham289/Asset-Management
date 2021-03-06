import { createApp } from 'vue';
import App from './App.vue';
import router from "./router/router";
import store from "./store/store";


import Header from "./components/layout/TheHeader.vue";
import Navbar from "./components/layout/TheNavbar.vue";
import Content from "./components/layout/TheContent.vue";

import Button from './components/base/basebutton.vue';
import Loading from './components/base/baseloading.vue';
import Alert from './components/base/basealert.vue';
import Toast from './components/base/basetoast.vue';
import Checkbox from './components/base/basecheckbox.vue';
import Dialog from './components/base/basedialog.vue';
import Input from './components/base/baseinput.vue';
import Spinner from './components/base/BaseSpinner.vue';
import Datepicker from './components/base/basedatepicker.vue';
import Combobox from './components/base/basecombobox.vue';
import Dropdown from './components/base/basedropdown.vue';
import Paginate from 'vuejs-paginate-next';

const app = createApp(App);
app.use(router);
app.use(store);
//
app.component("HUSTHeader", Header);
app.component("HUSTContent", Content);
app.component("HUSTNavbar", Navbar);
//
app.component('BaseButton', Button);
app.component('BaseLoading', Loading);
app.component('BaseAlert', Alert);
app.component('BaseToast', Toast);
app.component('BaseCheckbox', Checkbox);
app.component('BaseDialog', Dialog);
app.component('BaseInput', Input);
app.component('DatePicker', Datepicker);
app.component('BaseCombobox', Combobox);
app.component('BaseDropdown', Dropdown);
app.component('BasePaginate', Paginate);
app.component('BaseSpinner', Spinner);

app.mount('#app');
