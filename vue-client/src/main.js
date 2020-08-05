import Vue from 'vue'
import App from './App.vue'
import router from './router'

import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import './custom.css'

import { BootstrapVue } from 'bootstrap-vue'

import axios from 'axios'
import VueAxios from 'vue-axios'

Vue.use(BootstrapVue)
Vue.use(VueAxios, axios)

Vue.config.productionTip = false

axios.defaults.baseURL = process.env.VUE_APP_API_HOST;

new Vue({
  router,
  render: (h) => h(App),
}).$mount('#app')
