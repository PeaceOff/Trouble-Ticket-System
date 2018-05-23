import Vue from 'vue'
import AuthProxy from '@/proxies/AuthProxy'
import {
  CHECK_LOGIN,
  LOGIN_SUCCESS,
  LOGOUT
} from './mutations-types'

export const check = ({ commit }) => {
  commit(CHECK_LOGIN)
}

export const login = async ({ commit }, data) => {
  try {
    const response = await new AuthProxy().login(data)
    commit(LOGIN_SUCCESS, response)

    Vue.router.push({
      name: 'unassigned_tickets'
    })

    return { success: true }
  } catch (e) {
    throw e
  }
}

export const logout = ({ commit, state }) => {
  localStorage.removeItem('token')
  commit(LOGOUT)
  Vue.router.push({
    name: 'home'
  })
}

export default {
  check,
  login,
  logout
}
