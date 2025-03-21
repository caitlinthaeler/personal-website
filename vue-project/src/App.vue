<script setup>
import { computed } from 'vue';
import { useRoute } from 'vue-router';
import DefaultLayout from './layouts/DefaultLayout.vue';
import AdminLayout from './layouts/AdminLayout.vue';
import MinimalLayout from './layouts/MinimalLayout.vue';

const route = useRoute();
const layout = computed(() => route.meta.layout)
</script>

<template>
      <component :is="layout" />
</template>