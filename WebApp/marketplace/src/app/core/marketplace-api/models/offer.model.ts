import { CategoryModel } from './category.model'
import { UserModel } from './user.model'

export class OfferModel {

  category?: CategoryModel
  categoryId?: number
  description?: string
  id?: string
  location?: string
  pictureUrl?: any
  publishedOn?: string
  title?: string
  user?: UserModel
  userId?: number
  // TODO: Complete the offer model
  constructor( ) {

  }
}
