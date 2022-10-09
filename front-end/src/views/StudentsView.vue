<!-- eslint-disable vue/valid-v-slot -->
<template>
  <div>
    <v-container
      class="spacing-playground pa-n20"
    >
      <v-container class="spacing-playground pa-n20">
        <v-row justify="center">
          <v-dialog
            v-model="dialog"
            persistent
            max-width="600px"
          >
            <template v-slot:activator="{ on, attrs }">
              <v-btn
                color="primary"
                dark
                v-bind="attrs"
                v-on="on"
              >
                Cadastrar Aluno
              </v-btn>
            </template>
            <v-card>
              <v-card-title>
                <span class="text-h5">Cadastrar Aluno</span>
              </v-card-title>
              <v-card-text>
                <v-container>
                  <v-row>
                    <v-col cols="12">
                      <v-text-field
                        label="Nome*"
                        required
                        v-model="name"
                        :error-messages="nameErrors"
                        @input="$v.name.$touch()"
                        @blur="$v.name.$touch()"
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12">
                      <v-text-field
                        label="Email*"
                        required
                        v-model="email"
                        :error-messages="emailErrors"
                        @input="$v.email.$touch()"
                        @blur="$v.email.$touch()"
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12">
                      <v-text-field
                        label="Registro Escolar (RA)*"
                        required
                        v-model="ra"
                        :error-messages="raErrors"
                        :disabled="btnDisableds[0]"
                        @input="$v.ra.$touch()"
                        @blur="$v.ra.$touch()"
                      ></v-text-field>
                    </v-col>
                    <v-col cols="12">
                      <v-text-field
                        label="CPF*"
                        required
                        v-model="cpf"
                        :error-messages="cpfErrors"
                        :disabled="btnDisableds[1]"
                        @input="$v.cpf.$touch()"
                        @blur="$v.cpf.$touch()"
                      ></v-text-field>
                    </v-col>
                  </v-row>
                </v-container>
                <small>*campos obrigatórios</small>
              </v-card-text>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn
                  color="blue darken-1"
                  text
                  @click="clear"
                >
                  Cancelar
                </v-btn>
                <v-btn
                  color="blue darken-1"
                  text
                  @click="submit"
                >
                  Salvar
                </v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </v-row>
        <v-row justify="center">
          <v-dialog
            v-model="dialogResponse"
            max-width="350"
          >
            <v-card>
              <v-card-title class="text-h7">
                {{messageResponse}}
              </v-card-title>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn
                  color="green darken-1"
                  text
                  @click="dialogResponse = false"
                >
                  Ok
                </v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </v-row>
      </v-container>
      <v-text-field
          v-model="search"
          label="Faça sua busca"
          class="mx-4"
      ></v-text-field>
    </v-container>
    <section>
      <div>
        <v-row justify="center">
          <v-dialog
            v-model="dialogDelete"
            max-width="450"
          >
            <v-card>
              <v-card-title class="text-h5">
                Tem certeza que deseja excluir este aluno?
              </v-card-title>
              <v-card-actions>
                <v-spacer></v-spacer>

                <v-btn
                  color="green darken-1"
                  text
                  @click="deselectStudent()"
                >
                  Cancelar
                </v-btn>

                <v-btn
                  color="green darken-1"
                  text
                  @click="deleteStudent()"
                >
                  Excluir
                </v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </v-row>
      </div>
      <v-data-table
        :headers="headers"
        :items="students"
        :items-per-page="5"
        class="elevation-1"
        :search="search"
      >
                            <template v-slot:item.actions="{item}" >
                              <v-btn
                                class="ma-2"
                                text
                                icon
                                color="blue lighten-2"
                                @click="updateStudent(item)"
                              >
                                <v-icon
                                  small
                                >
                                  mdi-pencil
                                </v-icon>
                              </v-btn>
                              <v-btn
                                class="ma-2"
                                text
                                icon
                                color="red lighten-2"
                                @click="selectStudent(item)"
                              >
                                <v-icon
                                  small
                                >
                                  mdi-delete
                                </v-icon>
                              </v-btn>
                            </template>
      </v-data-table>
    </section>
    <SidebarComponent titleView="Consulta de Alunos"/>
  </div>
</template>

<script>
  import { validationMixin } from 'vuelidate'
  import { required, email, numeric, minLength, maxLength } from 'vuelidate/lib/validators'
  import SidebarComponent from '../components/SidebarComponent.vue'
  // import DialogComponent from '../components/DialogComponent.vue'
  import { GetAll, Post, Put, Delete } from '../services/requestApi'

  export default {
    mixins: [validationMixin],
  
    validations: {
      name: { required },
      email: { required, email },
      ra: { required, numeric },
      cpf: { required, minLength: minLength(11), maxLength: maxLength(11), numeric },
    },

    components: {
      SidebarComponent,
      // DialogComponent,
    },
  
    mounted() {
      this.getStudents()
    },

    computed: {
      nameErrors () {
        const errors = []
        if (!this.$v.name.$dirty) return errors
        !this.$v.name.required && errors.push('Campo obrigatório')
        return errors
      },
      emailErrors () {
        const errors = []
        if (!this.$v.email.$dirty) return errors
        !this.$v.email.email && errors.push('Deve ser e-mail válido')
        !this.$v.email.required && errors.push('Campo obrigatório')
        return errors
      },
      raErrors () {
        const errors = []
        if (!this.$v.ra.$dirty) return errors
        !this.$v.ra.required && errors.push('Campo obrigatório')
        !this.$v.ra.numeric && errors.push('RA deve conter conter somente números')
        return errors
      },
      cpfErrors () {
        const errors = []
        if (!this.$v.cpf.$dirty) return errors
        !this.$v.cpf.required && errors.push('Campo obrigatório')
        !this.$v.cpf.numeric && errors.push('CPF deve conter conter somente números')
        !this.$v.cpf.minLength && errors.push('CPF deve ter 11 caractéres')
        !this.$v.cpf.maxLength && errors.push('CPF deve ter 11 caractéres')
        return errors
      },
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
          {
            text: 'Ações',
            value: 'actions',
            align: 'center',
          },
        ],
        students: [],
        dialog: false,
        dialogResponse: false,
        messageResponse: '',
        name: '',
        email: '',
        ra: '',
        cpf: '',
        btnDisableds: [false, false],
        dialogDelete: false,
        studentSelected: {},
        updated: false,
      }
    },

    methods: {
      async getStudents () {
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
          this.students = data
        } catch (error) {
          console.log(error)
        }
      },
      selectStudent (student) {
        this.studentSelected = student
        this.dialogDelete = true
      },
      deselectStudent () {
        this.studentSelected = {}
        this.dialogDelete = false
      },
      async deleteStudent () {
        await Delete(this.studentSelected.ra)
        await this.getStudents()
        this.dialogDelete = false
      },
      updateStudent (student) {
        this.updated = true
        this.name = student.name
        this.email = student.email
        this.ra = student.ra
        this.cpf = student.cpf
        this.btnDisableds = [true, true]
        this.dialog = true
      },
      submit () {
        if (this.updated) return this.put();
        return this.post()
      },
      async post () {
        try {
          this.$v.$touch()
          await Post({
            ra: this.ra,
            name: this.name,
            email: this.email,
            cpf: this.cpf,
          })
          this.messageResponse = "Aluno cadastrado com sucesso"
          this.clear()
          this.dialogResponse = true
          this.getStudents();
        } catch (error) {
          console.log(error)
          this.messageResponse = "Erro! Por favor, revise os campos e tente novamente!"
          this.dialogResponse = true;
        }
      },
      async put () {
        try {
          this.$v.$touch()
          await Put(
            this.ra,
            { name: this.name, email: this.email }
          )
          this.messageResponse = "Atualização feita com sucesso"
          this.dialogResponse = true
          this.clear()
          this.getStudents();
        } catch (error) {
          console.log(error)
          this.messageResponse = "Erro! Por favor, revise os campos e tente novamente!"
          this.dialogResponse = true;
        }
      },
      clear () {
        this.$v.$reset()
        this.name = ''
        this.email = ''
        this.ra = ''
        this.cpf = ''
        this.dialog = false
        this.updated = false
        this.btnDisableds = [false, false]
      },
    },
  }
</script>