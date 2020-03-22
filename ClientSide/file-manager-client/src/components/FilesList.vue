<template>
  <div class="container">
    <b-table striped hover :items="files" :fields="fields">
      <template slot="buttons" slot-scope="data">
        <b-button
          size="sm"
          :disabled="!data.item.contentUrl && !data.item.contentText"
          v-b-modal.modal-content
          v-on:click="selectFile(data.item)"
        >Показать</b-button>
        <b-button size="sm" v-on:click="deleteFile(data.item)">Удалить</b-button>
      </template>
    </b-table>
    <b-form-file
      v-model="uploadedFile"
      placeholder="Выберите файл"
      drop-placeholder="Drop file here..."
      name="data"
    ></b-form-file>
    <b-modal id="modal-content" :title="selectedFile.name" hide-footer>
      <b-img v-if="selectedFile.contentUrl" :src="selectedFile.contentUrl" fluid alt="Изображение"></b-img>
      <p v-if="selectedFile.contentText">{{selectedFile.contentText}}</p>
    </b-modal>
  </div>
</template>

<script>
import FilesService from "@/api-services/files.service";
export default {
  name: "FilesList",
  data: function() {
    return {
      fields: [
        { key: "name", label: "Имя файла" },
        { key: "formattedSize", label: "Размер файла" },
        { key: "creationDate", label: "Дата создания" },
        { key: "buttons", label: "" }
      ],
      files: null,
      uploadedFile: null,
      selectedFile: { name: "" }
    };
  },
  watch: {
    uploadedFile: function() {
      if (this.uploadedFile) {
        this.uploadFile();
      }
    }
  },
  methods: {
    getFiles: function() {
      FilesService.getAllFiles()
      .then(response => {
        console.log(response.data);
        this.files = response.data;
      })
      .catch(error => {
        console.log(error.response);
      });
    },
    uploadFile: function() {
      var self = this;
      FilesService.uploadFile(this.uploadedFile)
        .then(function(response) {
          var file = response.data;
          if (file) {
            self.files.push(file);
          }
          self.uploadedFile = null;
        })
        .catch(function(error) {
          alert("Не удалось загрузить файл " + self.uploadedFile.name);
        });
    },
    deleteFile: function(item) {
      var self = this;
      FilesService.deleteFile(item.name)
        .then(function(response) {
          var index = self.files.indexOf(item);
          if (index > -1) {
            self.files.splice(index, 1);
          }
        })
        .catch(function(error) {
          alert("Не удалось удалить файл " + item.name);
        });
    },
    selectFile: function(item) {
      this.selectedFile = item;
    }
  },
  created() {
    this.getFiles();
  }
};
</script>

<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
button {
  margin: 5px;
}
.container {
  width: 70%;
}
</style>
