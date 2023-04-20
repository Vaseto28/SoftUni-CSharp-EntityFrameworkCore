﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicHub.Data.Models;

public class Album
{
	public Album()
	{
		this.Songs = new HashSet<Song>();
	}

	[Key]
	public int Id { get; set; }

	[Required]
	[MaxLength(ValidationConstraints.AlbumNameMaxLength)]
	public string Name { get; set; }

	[Required]
	public DateTime ReleaseDate { get; set; }

	public decimal Price => this.Songs.Sum(x => x.Price);

	[ForeignKey(nameof(Producer))]
	public int? ProducerId { get; set; }

	public virtual Producer Producer { get; set; }

	public virtual ICollection<Song> Songs { get; set; }
}

