<template>
  <v-app>
    <SidebarComponent titleView="Cadastrar Aluno"/>
    <v-main>
      <p>Cadastrar Aluno</p>
    </v-main>
</v-app>
</template>

<script>
  import SidebarComponent from '../components/SidebarComponent.vue'

  export default {
    components: {
      SidebarComponent,
    },
    mounted() {
      console.log(`the component is now mounted.`)
  }
}
</script>