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

const deleteUnit = async () => {
    await handleApiResult<Unit>({
        result: unitsService.delete(unit.value!.id!),
        errorsRef: errors,
        router,
        fallbackRedirect: 'Units',
        successRedirect: 'Units'
    });
};
</script>

<template>
    <h1>Delete</h1>

    <ConditionalContent :errors="errors" :expected-content="unit">
        <div>
            <h3>Are you sure you want to delete this?</h3>
            <div>
                <h4>Ingredient type</h4>
                <hr>
                <Details :unit="unit!" />
                <form method="post">
                    <input @click.prevent="deleteUnit" type="submit" value="Delete" class="btn btn-danger"> |
                    <RouterLink :to="{name: 'Units'}">Back to List</RouterLink>
                </form>
            </div>
        </div>
    </ConditionalContent>
</template>

<style scoped></style>