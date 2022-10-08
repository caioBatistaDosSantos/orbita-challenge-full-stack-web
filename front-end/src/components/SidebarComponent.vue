<template>
  <v-app id="inspire">

    <v-navigation-drawer
      v-model="drawer"
      app
    >
      <v-sheet
        color="grey lighten-4"
        class="pa-4"
      >
        <v-avatar
          class="mb-4"
          color="grey darken-1"
          size="64"
        ></v-avatar>

        <div>john@vuetifyjs.com</div>
      </v-sheet>
      <v-divider></v-divider>
      <v-list>
        <v-list-item
          v-for="[link, text] in links"
          :key="link"
          :to="link"
          link
        >
          <!-- colocar icones aqui -->
          <!-- <v-list-item-icon>
            <v-icon>{{ icon }}</v-icon>
          </v-list-item-icon> -->
          <v-list-item-content>
            <v-list-item-title>{{ text }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-navigation-drawer>
    <v-app-bar app>
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>

      <v-toolbar-title>{{ titleView }}</v-toolbar-title>
    </v-app-bar>
  </v-app>
</template>

<script>
  export default {
    name: 'SidebarComponent',
    props: {
      titleView: String
    },
    data: () => ({
      drawer: null,
      links: [
        ['/', 'Consulta de Alunos'],
        ['/create', 'Cadastrar Aluno'],
      ],
    }),
  }
</script>