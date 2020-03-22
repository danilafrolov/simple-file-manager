import Vue from 'vue'
import App from './App.vue'
import Axios from 'axios';
import BootstrapVue from 'bootstrap-vue'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

Vue.config.productionTip = false
Vue.use(BootstrapVue)

Axios.defaults.baseURL = "http://localhost:5000/Files";

new Vue({
  render: h => h(App),
}).$mount('#app')
