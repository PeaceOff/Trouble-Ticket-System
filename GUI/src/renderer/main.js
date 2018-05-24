import Vue from 'vue'
import { router } from './router'
import store from './store'

import App from './App'
import './config/axios'

if (!process.env.IS_WEB) Vue.use(require('vue-electron'))
Vue.config.productionTip = false

/* eslint-disable no-new */
new Vue({
  router,
  store,
  components: { App },
  template: '<App/>'
}).$mount('#app')
