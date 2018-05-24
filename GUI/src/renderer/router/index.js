import Vue from 'vue'
import VueRouter from 'vue-router'
import store from '../store'

Vue.use(VueRouter)

// Authenticated route
const ifAuthenticated = (to, from, next) => {
  if (store.getters.isLoggedIn) {
    next()
    return
  }
  next('/login')
}

const routes = [
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
    path: '/unassigned_tickets',
    name: 'unassigned_tickets',
    component: () =>
      import('@/components/DepartmentIT/UnassignedTickets'),
    beforeEnter: ifAuthenticated
  },
  {
    path: '/my_tickets',
    name: 'my_tickets',
    component: () =>
      import('@/components/DepartmentIT/MyTickets'),
    beforeEnter: ifAuthenticated
  },
  {
    path: '/my_secondary_tickets',
    name: 'my_secondary_tickets',
    component: () =>
      import('@/components/DepartmentIT/SecondaryTickets'),
    beforeEnter: ifAuthenticated
  },
  {
    path: '/login',
    name: 'login',
    component: () =>
      import('@/components/Auth/Login')
  },
  {
    path: '/secondaryTicket/:id',
    name: 'secondaryTicket',
    component: () =>
      import('@/components/SecondaryTicket')
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

export const router = new VueRouter({
  routes,
  mode: 'hash'
})

Vue.router = router

export default {
  router
}
