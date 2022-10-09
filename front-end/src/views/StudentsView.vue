<template>
  <div>
    <section>
      <v-text-field
          v-model="search"
          label="Faça sua busca"
          class="mx-4"
      ></v-text-field>
      <DialogComponent />
      <v-data-table
        :headers="headers"
        :items="students"
        :items-per-page="5"
        class="elevation-1"
        :search="search"
      ></v-data-table>
    </section>
    <SidebarComponent titleView="Consulta de Alunos"/>
    </div>
</template>

<script>
  import SidebarComponent from '../components/SidebarComponent.vue'
  import DialogComponent from '../components/DialogComponent.vue'
  import { GetAll } from '../services/requestApi'

  export default {
    components: {
      SidebarComponent,
      DialogComponent,
    },
    async mounted() {
      try {
        const students = await GetAll();
        console.log(students)
        var data = [];
        students.forEach(e => {
          data.push(
            {
              ...e,
              actions: 'editar/excluir',
            }
          )
        });

        console.log(data)
        this.students = data;
      } catch (error) {
        console.log(error);
      }
    },
    update() {
      console.log('atualizando')
    },
    data () {
      return {
        search: '',
        headers: [
          {
            text: 'Registro Acadêmico',
            align: 'start',
            sortable: false,
            class: 'darken',
            value: 'ra',
          },
          { text: 'Nome', value: 'name' },
          { text: 'Email', value: 'email' },
          { text: 'CPF', value: 'cpf' },
          { text: 'Ações', value: 'actions' },
        ],
        students: [],
      }
    },
  }
</script>