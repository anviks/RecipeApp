<script setup lang="ts">
import type { Optional, Unit } from '@/types';
import { onMounted, ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import { handleApiResult } from '@/helpers/apiUtils';
import ConditionalContent from '@/components/ConditionalContent.vue';
import useServices from '@/helpers/useServices';
import Details from '@/views/units/partials/Details.vue';

const { unitsService, ingredientTypesService } = useServices();

const unit = ref<Optional<Unit>>(null);
const errors = ref<string[]>([]);

const route = useRoute();
const router = useRouter();
const id = route.params.id.toString();

onMounted(async () => {
    await handleApiResult<Unit>({
        result: unitsService.findById(id),
        dataRef: unit,
        errorsRef: errors,
        router,
        fallbackRedirect: 'Units'
    });

    if (unit.value?.ingredientTypeId) {
        unit.value.ingredientType = (await ingredientTypesService.findById(unit.value.ingredientTypeId)).data!;
    }
});
</script>

<template>
    <h1>Details</h1>
    <div>
        <h4>Unit</h4>
        <hr>
        <ConditionalContent :errors="errors" :expected-content="unit">
            <Details :unit="unit!" />
        </ConditionalContent>
    </div>
    <div>
        <RouterLink :to="{name: 'UnitEdit', params: {id}}">Edit</RouterLink>
        |
        <RouterLink :to="{name: 'Units'}">Back to List</RouterLink>
    </div>
</template>

<style scoped></style>