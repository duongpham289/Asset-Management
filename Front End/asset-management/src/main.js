import { createApp } from 'vue';
import App from './App.vue';
import Button from './components/base/basebutton.vue';
import Checkbox from './components/base/basecheckbox.vue';
import Dialog from './components/base/basedialog.vue';
import Input from './components/base/baseinput.vue';
import Datepicker from './components/base/basedatepicker.vue';
import Combobox from './components/base/basecombobox.vue';
import Dropdown from './components/base/basedropdown.vue';
import Paginate from 'vuejs-paginate-next';

const app = createApp(App);
app.component('BaseButton', Button);
app.component('BaseCheckbox', Checkbox);
app.component('BaseDialog', Dialog);
app.component('BaseInput', Input);
app.component('DatePicker', Datepicker);
app.component('BaseCombobox', Combobox);
app.component('BaseDropdown', Dropdown);
app.component('BasePaginate', Paginate);

app.mount('#app');
