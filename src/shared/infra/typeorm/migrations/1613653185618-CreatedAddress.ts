import {MigrationInterface, QueryRunner, Table} from "typeorm";

export default class CreatedAddress1613653185618 implements MigrationInterface {

    public async up(queryRunner: QueryRunner): Promise<void> {
      await queryRunner.createTable(
        new Table({
          name:'user_address',
          columns:[
            {
              name: 'id',
              type: 'uuid',
              isPrimary: true,
              generationStrategy: 'uuid',
              default: 'uuid_generate_v4()',
            },
            {
              name: 'street',
              type: 'varchar',
            },
            {
              name: 'district',
              type: 'varchar',
            },
            {
              name: 'city',
              type: 'varchar',
            },
            {
              name: 'county',
              type: 'varchar',
            },
            {
              name: 'zipCode',
              type: 'varchar',
            },
            {
              name: 'latitude',
              type: 'varchar',
            },
            {
              name: 'longitude',
              type: 'varchar',
            },
            {
              name: 'created_at',
              type: 'timestamp',
              default: 'now()',
            },
            {
              name: 'updated_at',
              type: 'timestamp',
              default: 'now()',
            },
          ],
        }),
      );
    }

    public async down(queryRunner: QueryRunner): Promise<void> {
      await queryRunner.dropTable('user_address');
    }

}
