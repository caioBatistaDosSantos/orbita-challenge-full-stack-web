<template>
  <div>
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
      max-width="290"
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
</div>
</template>

<script>
  import { validationMixin } from 'vuelidate'
  import { required, email, numeric, minLength, maxLength } from 'vuelidate/lib/validators'
  import { Post } from '../services/requestApi'

  export default {
    mixins: [validationMixin],

    validations: {
      name: { required },
      email: { required, email },
      ra: { required, numeric },
      cpf: { required, minLength: minLength(11), maxLength: maxLength(11), numeric },
    },

    data: () => ({
      dialog: false,
      dialogResponse: false,
      messageResponse: '',
      name: '',
      email: '',
      ra: '',
      cpf: ''
    }),

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

    methods: {
      async submit () {
        try {
          this.$v.$touch()
          await Post({
            ra: this.ra,
            name: this.name,
            email: this.email,
            cpf: this.cpf,
          })
          this.messageResponse = "Aluno cadastrado com sucesso"
          this.dialogResponse = true;
          this.clear()
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
      },
    },
  }
</script>