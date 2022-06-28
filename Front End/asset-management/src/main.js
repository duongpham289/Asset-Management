import { createApp } from 'vue';
import App from './App.vue';
import Button from './components/base/basebutton.vue';
import Checkbox from './components/base/basecheckbox.vue';
import Dialog from './components/base/basedialog.vue';
import Input from './components/base/baseinput.vue';
import Datepicker from './components/base/basedatepicker.vue';

const app = createApp(App);
app.component('BaseButton', Button);
app.component('BaseCheckbox', Checkbox);
app.component('BaseDialog', Dialog);
app.component('BaseInput', Input);
app.component('Datepicker', Datepicker);

app.mount('#app');
