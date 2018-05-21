import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)

export default new VueRouter({
  routes: [
    {
      path: '/home',
      name: 'home',
      component: () =>
        import('@/components/Home')
    },
    {
      path: '/department',
      name: 'department',
      component: () =>
        import('@/components/Department')
    },
    {
      path: '/department_it',
      name: 'department_it',
      component: () =>
        import('@/components/DepartmentIT')
    },
    {
      path: '/login',
      name: 'login',
      component: () =>
        import('@/components/Auth/Login')
    },
    {
      path: '/',
      redirect: '/home'
    },
    {
      path: '*',
      redirect: '/home'
    }
  ]
})
