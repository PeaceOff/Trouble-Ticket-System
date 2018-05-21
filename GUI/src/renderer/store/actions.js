import Vue from 'vue'
import API from '@/logic/proxy'
import {
  LOGIN,
  // LOGIN_SUCCESS,
  LOGOUT
} from './mutations-types'

export const login = async ({ commit }, data) => {
  commit(LOGIN)
  try {
    const response = await API.login(data)
    // commit(LOGIN_SUCCESS, response.data);
    /* Vue.router.push({
    name: 'department_it',
    }); */
    console.log(response)
    // localStorage.setItem("token", "JWT");
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
  login,
  logout
}
