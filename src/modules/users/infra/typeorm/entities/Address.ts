import {
  Entity,
  Column,
  PrimaryGeneratedColumn,
  CreateDateColumn,
  UpdateDateColumn,
} from 'typeorm';

Entity('user_address');
class Address {
  @PrimaryGeneratedColumn('uuid')
  id: string;

  @Column()
  street: string;

  @Column()
  district: string;

  @Column()
  city: string;

  @Column()
  county: string;

  @Column()
  zipCode: string;

  @Column()
  latitude: string;

  @Column()
  longitude: string;

  @CreateDateColumn()
  createdAt: Date;

  @UpdateDateColumn()
  updatedAt: Date;
}

export default Address;
