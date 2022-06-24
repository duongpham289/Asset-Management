import { createApp } from 'vue';
import App from './App.vue';
import Button from './components/base/basebutton.vue';
import Checkbox from './components/base/basecheckbox.vue';

const app = createApp(App);
app.component('BaseButton', Button);
app.component('BaseCheckbox', Checkbox);

app.mount('#app');
