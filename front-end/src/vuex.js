import Vue from 'vue'
import Vuex from 'vuex'

import { GetAll } from './services/requestApi'

Vue.use(Vuex)

const store = new Vuex.Store({
  state: {
    students: []
  },
  mutations: {
    getStudents: async state => {
      try {
        const response = await GetAll();
        var data = [];
        response.forEach(e => {
          data.push(
            {
              ...e,
              actions: 'editar/excluir',
            }
          )
        });
  
        console.log(data)
        state.students = data;
      } catch (error) {
        console.log(error)
      }
    }
  }
})

export { store }
