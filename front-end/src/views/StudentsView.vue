<template>
  <v-app>
    <SidebarComponent titleView="Consulta de Alunos"/>
    <v-main>
      <p>Consulta de Alunos</p>
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