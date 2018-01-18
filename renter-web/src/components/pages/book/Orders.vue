<template>
<section>
  <b-loading :active.sync="loading" :canCancel="false"></b-loading>
  <button class="button is-outlined is-danger is-medium" @click="checkedRows = []" :disabled="!checkedRows.length">
          <b-icon icon="close"></b-icon>
          <span>Clear checked</span>
      </button>

  <button class="button is-outlined is-primary is-medium" @click="rentBooks" :disabled="!checkedRows.length">
        <b-icon icon="check"></b-icon>
        <span>Confirm rent</span>
      </button>

      <button class="button is-outlined is-info is-medium" @click="returnBooks" :disabled="!checkedRows.length">
            <b-icon icon="angle-double-down "></b-icon>
            <span>Confirm return</span>
          </button>

  <b-table :data="formattedRentedBooks" :checked-rows.sync="checkedRows" :row-class="rowClass" checkable>

    <template slot-scope="props">
            <b-table-column label="Title">
                {{ props.row.book.title }}
            </b-table-column>
            <b-table-column label="User">
                {{ props.row.user.name }} {{ props.row.user.surname }}
            </b-table-column>
            <b-table-column label="From">
                {{ new Date(props.row.from).toLocaleDateString("pl-PL") }}
            </b-table-column>
            <b-table-column label="To">
                {{ new Date(props.row.to).toLocaleDateString("pl-PL") }}
            </b-table-column>
            <b-table-column label="State">
                {{ props.row.received ? 'Owned' : 'Ready to pickup' }}
            </b-table-column>
          </template>
  </b-table>
</section>
</template>

<script>
import Moment from 'moment';
import axios from 'axios';
export default {
  data () {
    return {
      hidden: true,
      checkedRows: [],
      loading: false
    };
  },
  created () {
    this.$store.dispatch('getAllRentedBooks');
  },
  methods: {
    handleClick () {

    },
    rowClass (row, index) {
      return row.state;
    },
    rentBooks () {
      this.loading = true;
      this.checkedRows.map((row) => {
        axios.post(`http://localhost:5000/api/book/confirm/${row.bookId}`, {}, {
          headers: {
            'Authorization': `Bearer ${this.$store.getters.token}`
          }
        }).then(() => {
          this.loading = false;
          this.$store.dispatch('getAllRentedBooks');
        });
      });
    },
    returnBooks () {
      this.loading = true;
      this.checkedRows.map((row) => {
        axios.post(`http://localhost:5000/api/book/confirm-return/${row.bookId}`, {}, {
          headers: {
            'Authorization': `Bearer ${this.$store.getters.token}`
          }
        }).then(() => {
          this.loading = false;
          this.$store.dispatch('getAllRentedBooks');
        });
      });
    }
  },
  computed: {
    formattedRentedBooks () {
      var books = this.$store.getters.allRentedBooks;
      var formattedBooks = [];
      books.map((book) => {
        let now = Moment();
        let to = Moment(new Date(book['to']));
        let diff = to.diff(now, 'days');

        if (diff <= 4 && diff > 0) {
          book['state'] = 'is-warning';
        }
        if (diff <= 0) {
          book['state'] = 'is-error';
        }

        formattedBooks.push(book);
      });

      return formattedBooks;
    }
  }
};
</script>
<style>
tr.is-warning {
  background: hsla(48, 100%, 67%, 0.5) !important;
}

tr.is-error {
  background: hsla(5, 99%, 50%, 0.5) !important;
}
</style>
