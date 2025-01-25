<script setup>
import { defineProps } from 'vue';

defineProps({
    bg: {
        type: String,
        default: 'bg-gray-100'
    }
    
});
</script>

<template>
    <div :class="`${bg} p-6 sharp-lg shadow-md bg-plum`">
        <slot></slot>
    </div>
</template>